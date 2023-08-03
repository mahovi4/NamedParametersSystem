using NamedParametersSystem;

namespace NamedParametersSystemWinControls;

public interface IParameterControl
{
    public event ParameterChangesHandler Change;

    public string ControlName { get; }
    public IParameter Parameter { get; }

    public void Init(IParameter parameter, int labelWidth = 0);
}

