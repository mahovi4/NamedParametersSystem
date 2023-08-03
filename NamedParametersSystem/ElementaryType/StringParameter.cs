namespace NamedParametersSystem;

public sealed class StringParameter : 
    Parameter<string, CustomParameterInfo<string>>, 
    IElementaryTypeParameter, IParameter
{
    private string? val;

    public override CustomParameterInfo<string> Info { get; }

    public override string Value
    {
        get => val ?? Info.DefaultValue;
        set
        {
            if (Info.ForbiddenValues.Contains(value))
            {
                OnError($"Значение {value} является не допустимым для данного параметра");
                return;
            }

            if(val is null || !val.Equals(value))
                OnChange(value);

            val = value;
        }
    }

    public StringParameter(CustomParameterInfo<string> info, string? value = default)
    {
        Info = info;
        Value = value ?? Info.DefaultValue;
    }
    public StringParameter()
        :this(new CustomParameterInfo<string>("")){}

    public override void ChangeInfo(CustomParameterInfo<string> info)
    {
        Info.ReadOnly = info.ReadOnly;
        Info.DefaultValue = info.DefaultValue;
        Info.ForbiddenValues = info.ForbiddenValues;
    }
}

