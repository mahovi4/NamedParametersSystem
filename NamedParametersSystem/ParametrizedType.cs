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
        if (!this.ContainsParameter(paramName) && !GetType().ContainsProperty(paramName))
        {
            OnError(paramName, $"Параметр с именем '{paramName}' не найден!");
            return;
        }

        foreach (var prop in GetType().GetProperties())
            if (prop.CanRead && prop.PropertyType.CheckOnInterface("IParameter"))
            {
                var val = prop.GetValue(this);
                if (val is not IParameter param)
                {
                    OnError(paramName, $"Параметр '{paramName}' вернул нулевое значение");
                    return;
                }

                if (!param.Name.Equals(paramName) && !prop.Name.Equals(paramName)) continue;

                param.Error += OnError;
                param.Change += OnChange;
                param.FromObj(value);
            }
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
        return from prop in GetType().GetProperties() 
            where prop.CanRead && prop.PropertyType.CheckOnInterface("IParameter") 
            select prop.GetValue(this)
            into val where val is not null 
            select (IParameter) val;
    }

    protected void OnError(string paramName, string message)
    {
        Error?.Invoke(paramName, message);
    }
    protected void OnChange(string paramName, object newValue)
    {
        Change?.Invoke(paramName, newValue);
    }
}

