using NamedParametersSystem;

namespace NamedParametersSystemWinControls
{
    public partial class CollectionParameterControl : UserControl, IParameterControl
    {
        public event ParameterChangesHandler? Change;

        public string ControlName { get; private set; }

        public IParameter Parameter =>
            param ?? throw new ArgumentNullException(nameof(Parameter));

        private ICollectionParameter? param;

        public CollectionParameterControl()
        {
            InitializeComponent();
            ControlName = "Список значений параметра";
        }

        public void Init(IParameter parameter, int labelWidth = 0)
        {
            if (parameter is not ICollectionParameter collParameter)
                throw new ArgumentException($"Параметр '{parameter}' не является параметром списка элементов!");

            param = collParameter;

            ttMain.RemoveAll();
            ControlName = param.Name;

            lbList.Items.Clear();
            if (param?.ToObj() is not IEnumerable<object> colObj)
                throw new ArgumentException($"Параметр '{param}' не является списком элементов!");

            lbList.Items.AddRange(colObj.ToArray());

            ttMain.SetToolTip(lbList, param.Description);
            ttMain.SetToolTip(btnAdd, $"Добавить '{param.ElementParameter.Name}' в список");
            ttMain.SetToolTip(btnDel, $"Удалить выделенный '{param.ElementParameter.Name}' из списка");
            ttMain.SetToolTip(btnClear, $"Очистить список '{param.Name}'");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (param is null) return;

            var dlg = new CustomControlDialog();
            dlg.Init(new CustomParameterControl(), param.ElementParameter);

            if (dlg.ShowDialog() != DialogResult.OK) return;

            var par = dlg.Control?.Parameter.ToObj();

            if (par is null) return;

            lbList.Items.Add(par);

            param.FromObj(lbList.Items.Cast<object>());

            OnChange(param.ToObj());
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (param is null || lbList.SelectedItems.Count == 0) return;

            lbList.Items.Remove(lbList.SelectedItems[0]);

            param.FromObj(lbList.Items.Cast<object>());

            OnChange(param.ToObj());
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (param is null) return;

            lbList.Items.Clear();

            param.FromObj(Array.Empty<object>());

            OnChange(Array.Empty<object>());
        }

        private void lbList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (param is null || lbList.SelectedItems.Count == 0) return;

            var selIndex = 0;
            for (var i = 0; i < lbList.Items.Count; i++)
                if (lbList.Items[i].Equals(lbList.SelectedItems[0]))
                    selIndex = i;

            var par = param.ElementParameter;
            par.FromObj(lbList.SelectedItems[0]);

            var dlg = new CustomControlDialog();
            dlg.Init(new CustomParameterControl(), par);

            if (dlg.ShowDialog() != DialogResult.OK) return;

            var old = lbList.Items[selIndex];

            var p = dlg.Control?.Parameter.ToObj();

            if (p is null || p.Equals(old)) return;

            lbList.Items[selIndex] = p;

            param.FromObj(lbList.Items.Cast<object>());

            OnChange(param.ToObj());
        }

        private void OnChange(object value)
        {
            Change?.Invoke(ControlName, value);
        }
    }
}
