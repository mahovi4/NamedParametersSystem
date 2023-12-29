using System.Reflection;

namespace NamedParametersSystem;

public static class ParametersHandler
{
    public static IParameterizedType GetDefaultValue(this Type type)
    {
        if (!type.CheckOnInterface("IParameterizedType"))
            throw new ArgumentException("Указанный тип не реализует интерфейс 'IParameterizedType'");

        if (type.IsAbstract)
            return (IParameterizedType)type
                .GetProperty("DefaultValue", BindingFlags.Public | BindingFlags.Static,
                    null, type, Type.EmptyTypes, Array.Empty<ParameterModifier>())
                ?.GetValue(null)!
                   ?? throw new ArgumentException("Указанный тип не имеет статического свойства 'DefaultValue'");

        return (IParameterizedType)type
            .GetConstructor(BindingFlags.Instance | BindingFlags.Public, Array.Empty<Type>())
            ?.Invoke(Array.Empty<object>())!
               ?? throw new ArgumentException("Указанный тип не имеет конструктора без параметров");
    }

    public static bool ContainsParameter(this IParameterizedType obj, string paramName)
    {
        return obj.Parameters
            .Any(param => param.Name.Equals(paramName));
    }

    public static IEnumerable<object> GetChild(this Type type)
    {
        if (!type.CheckOnInterface("IParameterizedType"))
            throw new ArgumentException("Указанный тип не реализует интерфейс 'IParameterizedType'");

        var collection = type
            .GetMethod("GetChild", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static, Array.Empty<Type>())
            ?.Invoke(null, null);

        return collection is not IEnumerable<object> col
            ? new List<object>()
            : col;
    }
}