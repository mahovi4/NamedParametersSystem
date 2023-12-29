using NamedParametersSystem;

namespace NamedParametersSystemWinControls;

public partial class BoolParameterControl : UserControl, IParameterControl
{
    public event ParameterChangesHandler? Change;
    public string ControlName { get; private set; }

    public IParameter Parameter => param;

    private BoolParameter param;

    public BoolParameterControl()
    {
        InitializeComponent();
        ControlName = "Логический параметр";
        param = new BoolParameter();
        Init(new BoolParameter());
    }

    public void Init(IParameter parameter, int labelWidth = 0)
    {
        if (parameter is not BoolParameter boolPar)
            throw new ArgumentException($"Параметр '{parameter}' не является логическим!");

        param.Change -= OnChange;

        param = boolPar;

        ttMain.RemoveAll();
        ControlName = param.Name;

        cbValue.Text = param.Name;
        cbValue.Enabled = !param.Info.ReadOnly;
        cbValue.Checked = param.Value;

        ttMain.SetToolTip(cbValue, param.Info.Description);

        param.Change += OnChange;
    }

    private void cbValue_CheckedChanged(object sender, EventArgs e)
    {
        param.Value = cbValue.Checked;
    }

    private void OnChange(string paramName, object value)
    {
        Change?.Invoke(paramName, value);
    }
}
