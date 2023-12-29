using NamedParametersSystem;

namespace NamedParametersSystemWinControls
{
    public partial class CustomParameterControl : UserControl, IParameterControl
    {
        public event ParameterChangesHandler? Change;

        public string ControlName { get; private set; }

        public IParameter Parameter =>
            param ?? throw new ArgumentNullException(nameof(Parameter));

        private ICustomTypeParameter? param;

        private readonly List<IParameterControl> parControls = new();
        private int maxLabelWidth = 0;

        public CustomParameterControl()
        {
            InitializeComponent();
            ControlName = "Параметры типа";
        }

        public void Init(IParameter parameter, int labelWidth = 0)
        {
            if (parameter is not ICustomTypeParameter ctParam)
                throw new ArgumentException();

            param = ctParam;
            Controls.Clear();
            parControls.Clear();
            ttMain.RemoveAll();
            ControlName = param.Name;

            Width = 200;
            Height = 130;

            maxLabelWidth = ctParam.ParamValue.Parameters
                .Select(par =>
                    TextRenderer.MeasureText(par.Name + ": ", new Label().Font).Width)
                .Prepend(0)
                .Max();

            var cList = new List<Control>();
            foreach (var par in ctParam.ParamValue.Parameters)
            {
                switch (par)
                {
                    case INumericParameter:
                        var nControl = new NumericParameterControl();
                        nControl.Init(par, maxLabelWidth);
                        nControl.Enabled = !par.ReadOnly;
                        parControls.Add(nControl);
                        nControl.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                        cList.Add(nControl);
                        break;
                    case BoolParameter bParam:
                        var bControl = new BoolParameterControl();
                        bControl.Init(bParam, maxLabelWidth);
                        bControl.Enabled = !bParam.ReadOnly;
                        parControls.Add(bControl);
                        bControl.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                        cList.Add(bControl);
                        break;
                    case StringParameter sParam:
                        var sControl = new StringParameterControl();
                        sControl.Init(sParam, maxLabelWidth);
                        sControl.Enabled = !sParam.ReadOnly;
                        sControl.Change += OnChange;
                        parControls.Add(sControl);
                        sControl.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                        cList.Add(sControl);
                        break;
                    case ISelectionParameter:
                        var selControl = new SelectionParameterControl();
                        selControl.Init(par, maxLabelWidth);
                        selControl.Enabled = !par.ReadOnly;
                        parControls.Add(selControl);
                        selControl.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                        cList.Add(selControl);
                        break;
                    case ICollectionParameter:
                        var cControl = new CollectionParameterControl();
                        cControl.Init(par, maxLabelWidth);
                        cControl.Enabled = !par.ReadOnly;
                        parControls.Add(cControl);
                        cControl.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
                        var gb = new GroupBox
                        {
                            Text = par.Name + ": ",
                            Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom,
                            Width = cControl.Width + 10,
                            Height = cControl.Height + 21
                        };
                        cControl.Left = 5;
                        cControl.Top = 16;
                        gb.Controls.Add(cControl);
                        cList.Add(gb);
                        break;
                    case ICustomTypeParameter:
                        var tControl = new CustomParameterControl();
                        tControl.Init(par, maxLabelWidth);
                        tControl.Enabled = !par.ReadOnly;
                        parControls.Add(tControl);
                        tControl.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
                        gb = new GroupBox
                        {
                            Text = par.Name + ": ",
                            Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom,
                            Width = tControl.Width + 10,
                            Height = tControl.Height + 21
                        };
                        tControl.Left = 5;
                        tControl.Top = 16;
                        gb.Controls.Add(tControl);
                        cList.Add(gb);
                        break;
                }
            }
            if (cList.Count == 0) return;

            int maxWidth = 0, fullHeight = 5;
            foreach (var control in cList)
            {
                if (control.Width > maxWidth)
                    maxWidth = control.Width;
                fullHeight += control.Height + 5;
            }

            Width = maxWidth + 10;
            Height = fullHeight;
            MinimumSize = new Size(Width, Height);

            int x = 5, y = 5;
            foreach (var control in cList)
            {
                control.Width = maxWidth;
                control.Left = x;
                control.Top = y;
                Controls.Add(control);
                y += control.Height + 5;
            }

            //Refresh();
        }

        private void ReInit()
        {
            if(param is null) return;

            //foreach(var par in param.ParamValue.Parameters)
            //    foreach(var control in parControls)
            //        if(control.ControlName.Equals(par.Name))
            //            control.Init(par, maxLabelWidth);

            foreach (var control in parControls)
                control.Change -= OnChange;

            foreach (var control in Controls)
                ((Control) control).Dispose();

            Controls.Clear();

            Init(param, maxLabelWidth);

            Refresh();
        }

        private void OnChange(string paramName, object value)
        {
            //var par = param?.ParamValue.GetParameter(paramName) ?? throw new ArgumentException();

            //foreach (var control in parControls)
            //    if(control.ControlName.Equals(paramName))
            //        control.Init(par, maxLabelWidth);

            //if (!param?.ParamValue.GetParameter(paramName).Equals(value) ?? throw new ArgumentException())
            //{
            //param?.ParamValue.SetParameter(paramName, value);
            //Init(param ?? throw new ArgumentException());
            Change?.Invoke(paramName, value);
            ReInit();
            //}
        }
    }
}
