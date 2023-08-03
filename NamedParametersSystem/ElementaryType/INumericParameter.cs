namespace NamedParametersSystem;

public interface INumericParameter
{
    public decimal MinValue { get; }
    public decimal MaxValue { get; }
    public decimal Increment { get; }
    public int DecimalPlaces { get; }
}

