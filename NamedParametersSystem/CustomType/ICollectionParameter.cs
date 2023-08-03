using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamedParametersSystem;

public interface ICollectionParameter : IParameter
{
    public int LimitCount { get; }

    public IParameter ElementParameter { get; }
}

