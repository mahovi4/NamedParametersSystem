namespace NamedParametersSystem;

public abstract class CollectionParameter<TParameterizedType, TParameterInfo> : IParameter
    where TParameterInfo : ParameterInfo
{
    public event ParameterMessageHandler? Error;
    public event ParameterChangesHandler? Change;

    public abstract TParameterInfo Info { get; }
    public abstract ICollection<TParameterizedType> Value { get; set; }

    public abstract void ChangeInfo(TParameterInfo info);

    public string Name => Info.Name;
    public string Description => Info.Description;
    public bool ReadOnly => Info.ReadOnly;

    public object ToObj()
    {
        return Value;
    }

    public void FromObj(object value)
    {
        try
        {
            Value = (ICollection<TParameterizedType>)value;
        }
        catch (Exception e)
        {
            OnError(e.Message);
        }
    }

    protected void OnError(string message)
    {
        Error?.Invoke(Info.Name, message);
    }

    protected void OnChange(object value)
    {
        Change?.Invoke(Info.Name, value);
    }

    #region OverridesRegion

    public override string ToString()
    {
        var col = Value.ToArray();
        var tmp = "";
        for (var i = 0; i < col.Length; i++)
        {
            if (col[i] == null) continue;
            if (i > 0) tmp += "; ";
            tmp += col[i]?.ToString();
        }
        return $"{Info.Name} = [{tmp}]";
    }

    public override int GetHashCode()
    {
        return ToString().GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        if (obj is Parameter<TParameterizedType, TParameterInfo> parameter)
            return parameter.GetHashCode().Equals(GetHashCode());
        return false;
    }

    #endregion
}

