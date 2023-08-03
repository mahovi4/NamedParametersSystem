using NamedParametersSystem;

namespace NamedParametersSystemWinControls;

public partial class SelectionParameterControl : UserControl, IParameterControl
{
    public event ParameterChangesHandler? Change;

    public string ControlName { get; private set; }

    public IParameter Parameter =>
        param ?? throw new ArgumentNullException(nameof(Parameter));

    private ISelectionParameter? param;

    public SelectionParameterControl()
    {
        InitializeComponent();
        ControlName = "Выбор значения параметра";
    }

    public void Init(IParameter parameter, int labelWidth = 0)
    {
        if (parameter is not ISelectionParameter selParameter)
            throw new ArgumentException($"Параметр '{parameter}' не является параметром выбора!");

        param = selParameter;

        ttMain.RemoveAll();
        ControlName = param.Name;

        lName.Text = param.Name + ": ";
        if (labelWidth > 0)
        {
            Width = labelWidth + 105;
            MinimumSize = new Size(Width, Height);
            lName.AutoSize = false;
            lName.Width = labelWidth;
            cbValue.Left = labelWidth + 5;
            cbValue.Width = 100;
        }

        if (param.IsStatic)
            cbValue.Items.AddRange(param.ElementType.GetChild().ToArray());
        else
            cbValue.Items.AddRange(Array.Empty<object>());

        cbValue.SelectedItem = param.ToObj();

        ttMain.SetToolTip(lName, param.Description);
        ttMain.SetToolTip(cbValue, param.Description);
    }

    private void cbValue_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(cbValue.SelectedIndex < 0 || param is null) return;

        if(cbValue.SelectedItem.Equals(param.ToObj())) return;

        param.FromObj(cbValue.SelectedItem);
        OnChange(cbValue.SelectedItem);
    }

    private void OnChange(object value)
    {
        Change?.Invoke(ControlName, value);
    }
}

