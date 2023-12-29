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

        if(param is not null) 
            param.Change -= OnChange;

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
        
        cbValue.Items.AddRange(param.Collection.ToArray());

        if(param.Collection.Contains(param.ToObj()))
            cbValue.SelectedItem = param.ToObj();
        else
            cbValue.SelectedIndex = 0;

        ttMain.SetToolTip(lName, param.Description);
        ttMain.SetToolTip(cbValue, param.Description);

        param.Change += OnChange;
    }

    private void cbValue_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    private void OnChange(string paramName, object value)
    {
        Change?.Invoke(paramName, value);
    }

    private void cbValue_Leave(object sender, EventArgs e)
    {
        if (cbValue.SelectedIndex < 0 || param is null) return;

        if (cbValue.SelectedItem.Equals(param.ToObj())) return;

        param.FromObj(cbValue.SelectedItem);
    }
}

