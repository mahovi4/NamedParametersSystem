using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamedParametersSystem
{
    public sealed class CollectionParameterInfo<TParameterizedType> : ParameterInfo
    {
        public int LimitCount { get; set; }

        public ICollection<TParameterizedType> DefaultValue { get; set; }

        public CustomParameterInfo<TParameterizedType> ElementInfo { get; set; }

        public CollectionParameterInfo()
        {
            DefaultValue = new List<TParameterizedType>();
            ElementInfo = new CustomParameterInfo<TParameterizedType>(
                (TParameterizedType) typeof(TParameterizedType).GetDefaultValue());
        }
    }
}
