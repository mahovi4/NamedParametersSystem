using Microsoft.VisualBasic.CompilerServices;

namespace NamedParametersSystem;

public sealed class DoubleParameter : 
    Parameter<double, NumericParameterInfo<double>>, 
    IElementaryTypeParameter, INumericParameter, IParameter
{
    private double val;

    public override NumericParameterInfo<double> Info { get; }

    public override double Value
    {
        get => val;
        set
        {
            if (value.LessDouble(Info.MinValue))
            {
                OnError($"Значение {value} меньше минимально допустимого {Info.MinValue}!");

                if(!val.EqualsDouble(Info.MinValue))
                    OnChange(Info.MinValue);

                val = Info.MinValue;
            }
            else if (value.MoreDouble(Info.MaxValue))
            {
                OnError($"Значение {value} больше максимально допустимого {Info.MaxValue}!");

                if(!val.EqualsDouble(Info.MaxValue))
                    OnChange(Info.MaxValue);

                val = Info.MaxValue;
            }
            else
            {
                if(!val.EqualsDouble(value))
                    OnChange(value);

                val = value;
            }
        }
    }

    public DoubleParameter(NumericParameterInfo<double> info, double value = 0)
    {
        Info = info;
        Value = value;
    }

    public DoubleParameter() 
        : this(new NumericParameterInfo<double>()){}

    public override void ChangeInfo(NumericParameterInfo<double> info)
    {
        Info.ReadOnly = info.ReadOnly;
        Info.DefaultValue = info.DefaultValue;
        Info.MaxValue = info.MaxValue;
        Info.MinValue = info.MinValue;
        Info.Increment = info.Increment;
        Info.DecimalPlaces = info.DecimalPlaces;
    }

    public decimal MinValue => (decimal)Info.MinValue;
    public decimal MaxValue => (decimal)Info.MaxValue;
    public decimal Increment => (decimal)Info.Increment;
    public int DecimalPlaces => Info.DecimalPlaces;

    public static implicit operator DoubleParameter(IntParameter parameter)
    {
        return new DoubleParameter(new NumericParameterInfo<double>
        {
            Name = parameter.Name,
            Description = parameter.Description,
            ReadOnly = parameter.ReadOnly,

            DefaultValue = parameter.Info.DefaultValue,
            MaxValue = parameter.Info.MaxValue,
            MinValue = parameter.Info.MinValue,
            Increment = parameter.Info.Increment,
            DecimalPlaces = 2
        }, parameter.Value);
    }
}

