using System.Windows.Forms;
using NamedParametersSystem;
using NamedParametersSystemWinControls;

var bend = new Bend(10, false, "38");

var bendPar = new CustomTypeParameter<Bend>(new CustomParameterInfo<Bend>(new Bend())
{
    Name = "Гиб",
    Description = "Параметры гиба",
    ReadOnly = false
}, bend);

var dlg = new CustomControlDialog();
dlg.Init(new CustomParameterControl(), bendPar);

IParameter? par = null;
if (dlg.ShowDialog() == DialogResult.OK)
    par = dlg.Control?.Parameter ?? throw new ArgumentNullException();

if (par is null) throw new ArgumentNullException();

class Bend : ParametrizedType
{
    public DoubleParameter Width { get; } = new(new NumericParameterInfo<double>(10)
    {
        Name = "Ширина",
        Description = "Ширина гиба",
        ReadOnly = false,
        
        MinValue = 10,
        MaxValue = 100,
        Increment = 0.25,
        DecimalPlaces = 2
    });

    public BoolParameter Bool { get; } = new(new CustomParameterInfo<bool>(true)
    {
        Name = "Бул",
        Description = "Булевая переменная",
        ReadOnly = false
    });

    public StringParameter String { get; } = new(new CustomParameterInfo<string>("12344")
    {
        Name = "Строка",
        Description = "Строковая переменная",
        ReadOnly = false,
        
        ForbiddenValues = new[]{"", "1", "6"}
    });

    public Bend()
    {
        Width.Error += OnError;
        Width.Change += OnChange;

        Bool.Error += OnError;
        Bool.Change += OnChange;

        String.Error += OnError;
        String.Change += OnChange;
    }

    public Bend(double width, bool b, string s) : this()
    {
        Width.Value = width;
        Bool.Value = b;
        String.Value = s;
    }
}