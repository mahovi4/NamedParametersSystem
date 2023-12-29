namespace NamedParametersSystem;

public interface IParameter
{
    public event ParameterMessageHandler Error;
    public event ParameterChangesHandler Change;

    public string Name { get; }
    public string Description { get; }
    public bool ReadOnly { get; }
    public GroupParameter? Group { get; }

    public object ToObj();
    public void FromObj(object value);
}