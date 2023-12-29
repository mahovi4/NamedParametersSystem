using System.Reflection;
using System.Windows.Forms;
using NamedParametersSystem;
using NamedParametersSystemWinControls;

var bend = new Bend(10, false, "38", new RightSide());

var bendPar = new CustomTypeParameter<Bend>(new CustomParameterInfo<Bend>(new Bend())
{
    Name = "Гиб",
    Description = "Параметры гиба",
    ReadOnly = false
}, bend);

var colBend = new CollectionTypeParameter<Bend>(new CollectionParameterInfo<Bend>()
{
    Name = "Гибы",
    Description = "Список всех гибов",
    ReadOnly = false,
    ElementInfo = new CustomParameterInfo<Bend>(new Bend())
    {
        Name = "Гиб",
        Description = "Параметры гиба",
        ReadOnly = false
    }
}, new []{bend, new Bend(25, true, "gfjhgj", new TopSide())});

var dlg = new CustomControlDialog();
dlg.Init(new CustomParameterControl(), bendPar);
//dlg.Init(new CollectionParameterControl(), colBend);

IParameter? par = null;
if (dlg.ShowDialog() == DialogResult.OK)
    par = dlg.Control?.Parameter ?? throw new ArgumentNullException();

if (par is null) throw new ArgumentNullException();

class Bend : ParametrizedType
{
    public double Width
    {
        get => width.Value;
        set => width.Value = value;
    }
    public bool Bool
    {
        get => @bool.Value;
        set => @bool.Value = value;
    }
    public string String
    {
        get => @string.Value; 
        set => @string.Value = value;
    }
    public Side Side
    {
        get => side.Value; 
        set => side.Value = value;
    }

    private DoubleParameter width = new(new NumericParameterInfo<double>(10)
    {
        Name = "Ширина",
        Description = "Ширина гиба",
        ReadOnly = false,
        
        MinValue = 10,
        MaxValue = 100,
        Increment = 0.25,
        DecimalPlaces = 2
    });
    private BoolParameter @bool = new(new CustomParameterInfo<bool>(true)
    {
        Name = "Бул",
        Description = "Булевая переменная",
        ReadOnly = false
    });
    private StringParameter @string = new(new CustomParameterInfo<string>("12344")
    {
        Name = "Строка",
        Description = "Строковая переменная",
        ReadOnly = false,
        
        ForbiddenValues = new[]{"", "1", "6"}
    });
    private SelectionParameter<Side> side = new(new SelectionParameterInfo<Side>(new LeftSide(), true)
    {
        Name = "Сторона",
        Description = "Параметры стороны",
        ReadOnly = false
    });

    public Bend()
    {
        width.Error += OnError;
        width.Change += OnChange;

        @bool.Error += OnError;
        @bool.Change += OnChange;

        @string.Error += OnError;
        @string.Change += OnChange;

        side.Error += OnError;
        side.Change += OnChange;
    }

    public Bend(double width, bool b, string s, Side side) : this()
    {
        this.width.Value = width;
        @bool.Value = b;
        @string.Value = s;
        this.side.Value = side;
    }
}

abstract class Side : ParametrizedType
{
    public string Name
    {
        get => name.Value;
        set => name.Value = value;
    }

    private StringParameter name = new(new CustomParameterInfo<string>("Левая")
    {
        Name = "Имя",
        Description = "Имя стороны",
        ReadOnly = true
    });

    protected Side(string name)
    {
        this.name.Value = name;
    }

    public static Side[] GetChild()
    {
        return (from type in Assembly.GetExecutingAssembly().GetTypes() 
            where type.BaseType == typeof(Side) 
            select (Side)type.GetConstructor(Type.EmptyTypes)
                ?.Invoke(Array.Empty<object>())!
            into side where side is not null
            select side)
            .ToArray();
    }

    public override string ToString()
    {
        return name.Value;
    }

    public override int GetHashCode()
    {
        return ToString().GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        if (obj is Side side)
            return side.GetHashCode().Equals(GetHashCode());
        return false;
    }
}

class LeftSide : Side
{
    public LeftSide() : base("Слева")
    {
    }
}

class RightSide : Side
{
    public RightSide() : base("Справа")
    {
    }
}

class TopSide : Side
{
    public TopSide() : base("Сверху")
    {
    }
}

class BottomSide : Side
{
    public BottomSide() : base("Снизу")
    {
    }
}