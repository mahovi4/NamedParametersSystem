using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamedParametersSystem;

public interface ISelectionParameter : IParameter
{
    public Type ElementType { get; }
    public bool IsStatic { get; }
}

