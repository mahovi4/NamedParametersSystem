namespace NamedParametersSystem;

public interface IParameterizedType
{
    public IEnumerable<IParameter> Parameters { get; }

    public void SetParameters(IEnumerable<IParameter> parameters);
    public void SetParameter(string paramName, object value);

    public object ToObj();
    public IParameterizedType FromObj(object value);
}

