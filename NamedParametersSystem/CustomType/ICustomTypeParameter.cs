namespace NamedParametersSystem;

public interface ICustomTypeParameter : IParameter
{
    public IParameterizedType ParamValue { get; }
}

