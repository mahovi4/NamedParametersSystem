namespace NamedParametersSystem;

public sealed class CustomTypeParameter<TParameterizedType> :
    Parameter<TParameterizedType, CustomParameterInfo<TParameterizedType>>, 
    IParameter, ICustomTypeParameter
    where TParameterizedType : IParameterizedType
{
    private TParameterizedType? val;

    public override CustomParameterInfo<TParameterizedType> Info { get; }

    public override TParameterizedType Value
    {
        get => val ?? Info.DefaultValue;
        set
        {
            if (Info.ForbiddenValues.Contains(value))
                OnError($"Значение {value} является не допустимым для данного параметра");

            if (val is null || !val.Equals(value))
                OnChange(value ?? throw new ArgumentNullException());

            val = value;
        }
    }

    public CustomTypeParameter(CustomParameterInfo<TParameterizedType> info, TParameterizedType? value = default)
    {
        Info = info;
        Value = value ?? Info.DefaultValue;
    }

    public CustomTypeParameter()
        : this(new CustomParameterInfo<TParameterizedType>()) { }

    public override void ChangeInfo(CustomParameterInfo<TParameterizedType> info)
    {
        Info.ReadOnly = info.ReadOnly;
        Info.DefaultValue = info.DefaultValue;
        Info.ForbiddenValues = info.ForbiddenValues;
    }

    public IParameterizedType ParamValue => Value;
}

