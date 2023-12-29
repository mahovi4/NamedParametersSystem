using NamedParametersSystem;

namespace NamedParametersSystemWinControls
{
    public partial class CustomControlDialog : Form
    {
        public IParameterControl? Control { get; private set; }

        private readonly int deltaWidth, deltaHeight;

        public CustomControlDialog()
        {
            InitializeComponent();
            deltaWidth = Width - scMain.Panel1.Width;
            deltaHeight = Height - scMain.Panel1.Height;
        }

        public void Init(IParameterControl control, IParameter parameter)
        {
            if (Control is not null) 
                Control.Change -= OnCtrlChanged;

            Control = control;
            Control.Init(parameter);

            Text = Control.ControlName;

            Width = ((Control)Control).Width + deltaWidth;
            Height = ((Control)Control).Height + deltaHeight;
            MinimumSize = new Size(Width, Height);

            ((Control)Control).Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            scMain.Panel1.Controls.Clear();
            scMain.Panel1.Controls.Add((Control)Control);

            Control.Change += OnCtrlChanged;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void OnCtrlChanged(string paramName, object newValue)
        {
            Init(Control!, Control!.Parameter);
            //Refresh();
        }
    }
}
