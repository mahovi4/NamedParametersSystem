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
            parControls.Clear();
            ttMain.RemoveAll();
            ControlName = param.Name;

            Width = 200;
            Height = 130;

            var maxLabelWidth = 0;
            var pars = param.ParamValue;
            foreach (var par in param.ParamValue.Parameters)
            {
                var width = TextRenderer.MeasureText(par.Name + ": ", new Label().Font).Width;
                if(width > maxLabelWidth)
                    maxLabelWidth = width;
            }

            //var maxLabelWidth = ctParam.ParamValue.Parameters
            //    .Select(par =>
            //        TextRenderer.MeasureText(par.Name + ": ", new Label().Font).Width)
            //    .Prepend(0)
            //    .Max();

            var cList = new List<Control>();
            foreach (var par in ctParam.ParamValue.Parameters)
            {
                switch (par)
                {
                    case INumericParameter:
                        var nControl = new NumericParameterControl();
                        nControl.Init(par, maxLabelWidth);
                        parControls.Add(nControl);
                        nControl.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                        nControl.Change += OnChange;
                        cList.Add(nControl);
                        break;
                    case BoolParameter bParam:
                        var bControl = new BoolParameterControl();
                        bControl.Init(bParam, maxLabelWidth);
                        parControls.Add(bControl);
                        bControl.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                        bControl.Change += OnChange;
                        cList.Add(bControl);
                        break;
                    case StringParameter sParam:
                        var sControl = new StringParameterControl();
                        sControl.Init(sParam, maxLabelWidth);
                        parControls.Add(sControl);
                        sControl.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                        sControl.Change += OnChange;
                        cList.Add(sControl);
                        break;
                    case ISelectionParameter:
                        var selControl = new SelectionParameterControl();
                        selControl.Init(par, maxLabelWidth);
                        parControls.Add(selControl);
                        selControl.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                        selControl.Change += OnChange;
                        cList.Add(selControl);
                        break;
                    case ICollectionParameter:
                        var cControl = new CollectionParameterControl();
                        cControl.Init(par, maxLabelWidth);
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
                        cControl.Change += OnChange;
                        gb.Controls.Add(cControl);
                        cList.Add(gb);
                        break;
                    case ICustomTypeParameter:
                        var tControl = new CustomParameterControl();
                        tControl.Init(par, maxLabelWidth);
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
                        tControl.Change += OnChange;
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
        }


        private void OnChange(string paramName, object value)
        {
            param?.ParamValue.SetParameter(paramName, value);
        }
    }
}
