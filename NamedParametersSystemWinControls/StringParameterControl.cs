using NamedParametersSystem;

namespace NamedParametersSystemWinControls;

public partial class StringParameterControl : UserControl, IParameterControl
{
    public event ParameterChangesHandler? Change;

    public string ControlName { get; private set; }

    public IParameter Parameter => param;

    private StringParameter param;

    public StringParameterControl()
    {
        InitializeComponent();
        ControlName = "Строковый параметр";
        param = new StringParameter();
        Init(param);
    }

    public void Init(IParameter parameter, int labelWidth = 0)
    {
        if (parameter is not StringParameter sParameter)
            throw new ArgumentException($"Параметр '{parameter}' не является строковым!");

        param.Change -= OnChange;

        param = sParameter;

        ttMain.RemoveAll();
        ControlName = param.Name;

        lName.Text = param.Name;
        if (labelWidth > 0)
        {
            Width = labelWidth + 105;
            MinimumSize = new Size(Width, Height);
            lName.AutoSize = false;
            lName.Width = labelWidth;
            tbValue.Left = labelWidth + 5;
            tbValue.Width = 100;
        }

        tbValue.Text = param.Value;
        tbValue.Enabled = !param.ReadOnly;

        ttMain.SetToolTip(lName, param.Description);
        ttMain.SetToolTip(tbValue, param.Description);

        param.Change += OnChange;
    }

    private void OnChange(string paramName, object value)
    {
        Change?.Invoke(paramName, value);
    }

    private void tbValue_Leave(object sender, EventArgs e)
    {
        param.Value = tbValue.Text;
    }
}