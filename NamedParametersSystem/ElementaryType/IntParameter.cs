namespace NamedParametersSystem;

public sealed class IntParameter : 
    Parameter<int, NumericParameterInfo<int>>, 
    IElementaryTypeParameter, INumericParameter, IParameter
{
    private int val;

    public override NumericParameterInfo<int> Info { get; }

    public override int Value
    {
        get => val;
        set
        {
            if (value < Info.MinValue)
            {
                OnError($"Значение {value} меньше минимально допустимого {Info.MinValue}!");

                if(val != Info.MinValue)
                    OnChange(Info.MinValue);

                val = Info.MinValue;
            }
            else if (value > Info.MaxValue)
            {
                OnError($"Значение {value} больше максимально допустимого {Info.MaxValue}!");

                if (val != Info.MaxValue)
                    OnChange(Info.MaxValue);

                val = Info.MaxValue;
            }
            else
            {
                if(val != value)
                    OnChange(value);

                val = value;
            }
        }
    }

    public IntParameter(NumericParameterInfo<int> info)
    {
        Info = info;
        Value = Info.DefaultValue;
    }
    public IntParameter(NumericParameterInfo<int> info, int value)
    {
        Info = info;
        Value = value;
    }
    public IntParameter() 
        : this(new NumericParameterInfo<int>()){}

    public override void ChangeInfo(NumericParameterInfo<int> info)
    {
        Info.ReadOnly = info.ReadOnly;
        Info.DefaultValue = info.DefaultValue;
        Info.MaxValue = info.MaxValue;
        Info.MinValue = info.MinValue;
        Info.DecimalPlaces = info.DecimalPlaces;
        Info.Increment = info.Increment;
    }

    public decimal MinValue => Info.MinValue;
    public decimal MaxValue => Info.MaxValue;
    public decimal Increment => Info.Increment;
    public int DecimalPlaces => Info.DecimalPlaces;

    public static implicit operator IntParameter(DoubleParameter parameter)
    {
        return new IntParameter(new NumericParameterInfo<int>
        {
            Name = parameter.Name,
            Description = parameter.Description,
            ReadOnly = parameter.ReadOnly,
            DefaultValue = parameter.Info.DefaultValue.Round(),
            MaxValue = parameter.Info.MaxValue.Round(),
            MinValue = parameter.Info.MinValue.Round(),
            Increment = parameter.Info.Increment.Round()
        }, parameter.Value.Round());
    }
}

