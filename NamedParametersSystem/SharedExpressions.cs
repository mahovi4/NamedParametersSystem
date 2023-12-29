using System.Reflection;

namespace NamedParametersSystem;

public static class SharedExpressions
{
    public static bool EqualsDouble(this double left, double right)
    {
        return Math.Abs(left - right) < 0.0000001;
    }

    public static bool LessDouble(this double left, double right)
    {
        return left < right - 0.0001;
    }

    public static bool MoreDouble(this double left, double right)
    {
        return left > right + 0.0001;
    }

    public static int Round(this double d)
    {
        return (int)Math.Round(d, 0, MidpointRounding.AwayFromZero);
    }

    public static double Round(this double d, int digits)
    {
        return Math.Round(d, digits, MidpointRounding.AwayFromZero);
    }

    public static bool CheckOnInterface(this Type type, string interfaceName)
    {
        if (type.Name.Equals(interfaceName)) return true;

        return type
            .GetInterfaces()
            .Any(iface => iface.Name == interfaceName);
    }

    public static bool ContainsProperty(this Type type, string propName)
    {
        return type.GetProperties()
            .Any(prop => prop.Name.Equals(propName));
    }

    public static bool ContainsField(this Type type, string fieldName)
    {
        return type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .Any(field => field.Name.Equals(fieldName));
    }
}

