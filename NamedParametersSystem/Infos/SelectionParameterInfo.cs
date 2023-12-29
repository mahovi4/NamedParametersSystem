using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamedParametersSystem;

public sealed class SelectionParameterInfo<TParameterizedType> : ParameterInfo
{
    public TParameterizedType DefaultValue { get; set; }
    public bool IsStatic { get; set; }
    public IEnumerable<TParameterizedType> Collection { get; set; }

    public SelectionParameterInfo(TParameterizedType defaultValue, bool isStatic = false, IEnumerable<TParameterizedType>? collection = null)
    {
        DefaultValue = defaultValue;

        IsStatic = isStatic;

        Collection = collection ?? new List<TParameterizedType>();
    }
}

