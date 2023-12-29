namespace NamedParametersSystem;

public sealed class SelectionParameter<TParameterizedType> : 
    Parameter<TParameterizedType, SelectionParameterInfo<TParameterizedType>>, 
    IParameter, ISelectionParameter
    where TParameterizedType : IParameterizedType
{
    private TParameterizedType? val;

    public override SelectionParameterInfo<TParameterizedType> Info { get; }

    public override TParameterizedType Value
    {
        get => val ?? Info.DefaultValue;
        set
        {
            if (value is null)
                throw new ArgumentNullException();

            var tmp = CheckValue(value);

            if (val is not null && val.Equals(tmp)) return;

            val = tmp;
            OnChange(tmp);
        }
    }

    public SelectionParameter(SelectionParameterInfo<TParameterizedType> info, TParameterizedType? value = default)
    {
        Info = info;

        Info.DefaultValue = CheckValue(Info.DefaultValue);

        Value = CheckValue(value);
    }

    public SelectionParameter() 
        : this(new SelectionParameterInfo<TParameterizedType>((TParameterizedType)typeof(TParameterizedType).GetDefaultValue())){}

    public override void ChangeInfo(SelectionParameterInfo<TParameterizedType> info)
    {
        Info.ReadOnly = info.ReadOnly;
        Info.DefaultValue = info.DefaultValue;
        Info.IsStatic = info.IsStatic;

        OnChange(Value);
    }
    
    public bool IsStatic => Info.IsStatic;

    public IEnumerable<object> Collection
    {
        get
        {
            if (Info.IsStatic)
            {
                var col = typeof(TParameterizedType).GetChild().ToArray();

                if(col.Length == 0)
                    throw new ArgumentException($"Коллекция параметра '{Info.Name}' не содержит элементов для выбора");

                return col;
            }
            
            if (!Info.Collection.Any())
                throw new ArgumentException($"Коллекция параметра '{Info.Name}' не содержит элементов для выбора");

            return Info.Collection.Cast<object>();
        }
    }

    private TParameterizedType CheckValue(TParameterizedType? value)
    {
        var col = Info.IsStatic
            ? typeof(TParameterizedType).GetChild().Cast<TParameterizedType>().ToList()
            : Info.Collection.ToList();

        if (value is null)
        {
            if (!col.Any()) 
                return Info.DefaultValue;

            return col.Contains(Info.DefaultValue) 
                ? Info.DefaultValue 
                : col[0];
        }

        if (!col.Any()) return value;

        if (col.Contains(value)) return value;

        return col.Contains(Info.DefaultValue) 
            ? Info.DefaultValue 
            : col[0];
    }
}