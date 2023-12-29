using System.Text.RegularExpressions;

namespace NamedParametersSystem;

public sealed class CustomParameterInfo<TParameterizedType> : ParameterInfo
{
    public TParameterizedType DefaultValue { get; set; }

    public IEnumerable<TParameterizedType> ForbiddenValues { get; set; }

    public string RegularExpression { get; set; }

    public CustomParameterInfo(TParameterizedType defaultValue)
    {
        DefaultValue = defaultValue;
        ForbiddenValues = Array.Empty<TParameterizedType>();
        RegularExpression = "";
    }
}

