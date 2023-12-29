using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamedParametersSystem;

public interface ISelectionParameter : IParameter
{
    public bool IsStatic { get; }
    public IEnumerable<object> Collection { get; }
}

