using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamedParametersSystem;

public interface ICollectionParameter : IParameter
{
    public int Count { get; }

    public int LimitCount { get; }

    public void Add(object element);

    public void Remove(object element);

    public void Clear();

    public IParameter ElementParameter { get; }

    public IEnumerable<object> ObjValue { get; set; }

    public object this[int index] { get; set; }
}