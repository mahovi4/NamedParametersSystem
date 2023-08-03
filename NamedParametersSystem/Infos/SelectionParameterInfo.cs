using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamedParametersSystem;

public sealed class SelectionParameterInfo<TParameterizedType> : ParameterInfo
{
    public TParameterizedType DefaultValue { get; set; }
    public Type ElementType => typeof(TParameterizedType);
    public bool IsStatic { get; set; }

    public SelectionParameterInfo(TParameterizedType? defaultValue = default, bool isStatic = false)
    {
        DefaultValue = defaultValue 
                       ?? (TParameterizedType)typeof(TParameterizedType).GetDefaultValue();
        IsStatic = isStatic;
    }
}

