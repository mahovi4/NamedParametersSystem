using NamedParametersSystem;

namespace NamedParametersSystemWinControls
{
    public partial class NumericParameterControl : UserControl, IParameterControl
    {
        public event ParameterChangesHandler? Change;

        public string ControlName { get; private set; }

        public IParameter Parameter => param;

        private DoubleParameter param;

        public NumericParameterControl()
        {
            InitializeComponent();
            ControlName = "Числовой параметр";
            param = new DoubleParameter();
            Init(param);
        }

        public void Init(IParameter parameter, int labelWidth = 0)
        {
            if (parameter is not INumericParameter numParam)
                throw new ArgumentException($"Параметр '{parameter}' не является числовым!");

            param = (DoubleParameter)numParam;

            ttMain.RemoveAll();

            ControlName = param.Name;

            lName.Text = param.Name + ": ";
            if (labelWidth > 0)
            {
                Width = labelWidth + 105;
                MinimumSize = new Size(Width, Height);
                lName.AutoSize = false;
                lName.Width = labelWidth;
                nudValue.Left = labelWidth + 5;
                nudValue.Width = 100;
            }

            nudValue.Minimum = param.MinValue;
            nudValue.Maximum = param.MaxValue;
            nudValue.Increment = param.Increment;
            nudValue.DecimalPlaces = param.DecimalPlaces;
            nudValue.Enabled = !param.ReadOnly;

            nudValue.Value = (decimal)param.Value;

            ttMain.SetToolTip(lName, param.Description);
            ttMain.SetToolTip(nudValue, param.Description);
        }

        private void nudValue_ValueChanged(object sender, EventArgs e)
        {
            param.Value = (double)nudValue.Value;
            OnChange(nudValue.Value);
        }

        private void OnChange(object value)
        {
            Change?.Invoke(param.Name, value);
        }
    }
}
