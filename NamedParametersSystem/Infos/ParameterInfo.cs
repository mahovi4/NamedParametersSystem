using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamedParametersSystem;

public abstract class ParameterInfo
{
    public event ParameterChangesHandler? Change;

    public string Name { get; set; }
    public string Description { get; set; }
    public bool ReadOnly { get; set; }
    public GroupParameter? Group { get; set; }

    protected ParameterInfo()
    {
        Name = "Безымянный параметр";
        Description = "Параметр, созданный по умолчанию";
        ReadOnly = false;
    }

    #region OverridesRegion

    public override string ToString()
    {
        return Name;
    }

    public override int GetHashCode()
    {
        return ToString().GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        if (obj is ParameterInfo info)
            return info.GetHashCode().Equals(GetHashCode());
        return false;
    }

    #endregion
}

