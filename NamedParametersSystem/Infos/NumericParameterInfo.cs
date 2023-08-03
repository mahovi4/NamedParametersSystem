namespace NamedParametersSystem;

public sealed class NumericParameterInfo<TParameterizedType> : ParameterInfo
    where TParameterizedType : IComparable<TParameterizedType>, new()
{
    public TParameterizedType MinValue { get; set; }
    public TParameterizedType MaxValue { get; set; }
    public TParameterizedType Increment { get; set; }
    public int DecimalPlaces { get; set; }
    public TParameterizedType DefaultValue { get; set; }

    public NumericParameterInfo()
    {
        MaxValue = new TParameterizedType();
        MinValue = new TParameterizedType();
        Increment = new TParameterizedType();
        DecimalPlaces = 2;
        DefaultValue = new TParameterizedType();
    }

    public NumericParameterInfo(TParameterizedType defaultValue) : this()
    {
        DefaultValue = defaultValue;
    }
}

