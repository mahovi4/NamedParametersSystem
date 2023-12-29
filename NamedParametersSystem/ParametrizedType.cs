using System.Reflection;

namespace NamedParametersSystem;

public abstract class ParametrizedType : IParameterizedType
{
    public event ParameterMessageHandler? Error;
    public event ParameterChangesHandler? Change;

    public IEnumerable<IParameter> Parameters => GetParameters();
    public void SetParameters(IEnumerable<IParameter> parameters)
    {
        foreach(var parameter in parameters)
            SetParameter(parameter.Name, parameter.ToObj());
    }
    public void SetParameter(string paramName, object value)
    {
        bool isField = false, 
            isProp = false, 
            isParam = false;

        isParam = this.ContainsParameter(paramName);

        if(!isParam)
            isProp = GetType().ContainsProperty(paramName);

        if(!isParam && !isProp)
            isField = GetType().ContainsField(paramName);

        if (!isParam && !isProp && !isField)
        {
            OnError(paramName, $"Параметр с именем '{paramName}' не найден!");
            return;
        }

        if (isParam) 
            foreach(var param in Parameters)
                if(param.Name == paramName)
                    param.FromObj(value);
        

        if (isProp)
            foreach (var prop in GetType().GetProperties())
                if (prop.Name == paramName)
                {
                    var val = prop.GetValue(this);
                    if (val is null)
                        throw new ArgumentNullException();

                    ((IParameter)val).FromObj(value);
                    prop.SetValue(this, val);
                }


        if(isField)
            foreach(var field in GetType().GetFields(
                        BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
                if (field.Name == paramName)
                {
                    var val = field.GetValue(this);
                    if(val is null)
                        throw new ArgumentNullException();

                    ((IParameter)val).FromObj(value);
                    field.SetValue(this, val);
                }
    }
    public IParameter GetParameter(string paramName)
    {
        foreach(var param in Parameters)
            if (param.Name.Equals(paramName))
                return param;
        throw new ArgumentException();
    }

    public object ToObj() => this;
    public IParameterizedType FromObj(object value)
    {
        if(!value.GetType().CheckOnInterface("IParameterizedType"))
            throw new ArgumentException($"Значение {value} не является параметром для {GetType().Name}!");

        return (IParameterizedType) value;
    }

    private IEnumerable<IParameter> GetParameters()
    {
        var list = new List<IParameter>();

        list.AddRange(SearchInFields());
        list.AddRange(SearchInProperties());

        return list;
    }

    protected void OnError(string paramName, string message)
    {
        Error?.Invoke(paramName, message);
    }
    protected void OnChange(string paramName, object newValue)
    {
        Change?.Invoke(paramName, newValue);
    }
    
    private IEnumerable<IParameter> SearchInFields()
    {
        var list = new List<IParameter>();

        list.AddRange(from field in GetType().BaseType?.GetFields(
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) 
            where field.FieldType.CheckOnInterface("IParameter") 
            select field.GetValue(this) 
            into val where val is not null 
            select (IParameter)val);

        list.AddRange(from field in GetType().GetFields(
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) 
            where field.FieldType.CheckOnInterface("IParameter") 
            select field.GetValue(this) 
            into val where val is not null 
            select (IParameter)val);

        return list;
    }

    private IEnumerable<IParameter> SearchInProperties()
    {
        var list = new List<IParameter>();

        list.AddRange(from field in GetType().BaseType?.GetProperties()
            where field.PropertyType.CheckOnInterface("IParameter")
            select field.GetValue(this)
            into val
            where val is not null
            select (IParameter)val);

        list.AddRange(from field in GetType().GetProperties()
            where field.PropertyType.CheckOnInterface("IParameter")
            select field.GetValue(this)
            into val
            where val is not null
            select (IParameter)val);

        return list;
    }
}

