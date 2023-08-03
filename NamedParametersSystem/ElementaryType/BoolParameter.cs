namespace NamedParametersSystem;

public sealed class BoolParameter : 
    Parameter<bool, CustomParameterInfo<bool>>, 
    IParameter, IElementaryTypeParameter
{
    private bool val;

    public override CustomParameterInfo<bool> Info { get; }

    public override bool Value
    {
        get => val;
        set
        {
            if(val != value)
                OnChange(value);

            val = value;
        }
    }

    public BoolParameter(CustomParameterInfo<bool> info)
    {
        Info = info;
        Value = Info.DefaultValue;
    }
    public BoolParameter(CustomParameterInfo<bool> info, bool value)
    {
        Info = info;
        Value = value;
    }
    public BoolParameter()
        : this(new CustomParameterInfo<bool>()){}

    public override void ChangeInfo(CustomParameterInfo<bool> info)
    {
        Info.ReadOnly = info.ReadOnly;
        Info.DefaultValue = info.DefaultValue;
        Info.ForbiddenValues = info.ForbiddenValues;
    }
}

