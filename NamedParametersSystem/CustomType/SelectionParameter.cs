namespace NamedParametersSystem;

public sealed class SelectionParameter<TParameterizedType> : 
    Parameter<TParameterizedType, SelectionParameterInfo<TParameterizedType>>, 
    IParameter, ISelectionParameter
{
    private TParameterizedType? val;

    public override SelectionParameterInfo<TParameterizedType> Info { get; }

    public override TParameterizedType Value
    {
        get => val ?? Info.DefaultValue;
        set
        {
            if(val is null || !val.Equals(value))
                OnChange(value ?? throw new ArgumentNullException());

            val = value;
        }
    }

    public SelectionParameter(SelectionParameterInfo<TParameterizedType> info, TParameterizedType? value = default)
    {
        Info = info;
        Value = value ?? Info.DefaultValue;
    }

    public SelectionParameter() 
        : this(new SelectionParameterInfo<TParameterizedType>()){}

    public override void ChangeInfo(SelectionParameterInfo<TParameterizedType> info)
    {
        Info.ReadOnly = info.ReadOnly;
        Info.DefaultValue = info.DefaultValue;
        Info.IsStatic = info.IsStatic;
    }

    public Type ElementType => Info.ElementType;
    public bool IsStatic => Info.IsStatic;
}

