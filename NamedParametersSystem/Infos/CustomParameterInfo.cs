namespace NamedParametersSystem;

public sealed class CustomParameterInfo<TParameterizedType> : ParameterInfo
{
    public TParameterizedType DefaultValue { get; set; }

    public IEnumerable<TParameterizedType> ForbiddenValues { get; set; }

    public CustomParameterInfo(TParameterizedType? defaultValue = default)
    {
        DefaultValue = defaultValue ?? (TParameterizedType)typeof(TParameterizedType).GetDefaultValue();
        ForbiddenValues = Array.Empty<TParameterizedType>();
    }
}

