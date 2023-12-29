namespace NamedParametersSystem;

public abstract class CollectionParameter<TParameterizedType, TParameterInfo> : IParameter, ICollectionParameter
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
    public GroupParameter? Group => Info.Group;

    public int Count => Value.Count;
    public abstract int LimitCount { get; }

    public abstract IParameter ElementParameter { get; }

    public IEnumerable<object> ObjValue
    {
        get => Value.Cast<object>();
        set
        {
            if (value is not IEnumerable<TParameterizedType> col)
                throw new ArgumentException($"Параметр '{value}' не является списком элементов '{nameof(TParameterizedType)}'!");
            Value = col.ToList();
        }
    }

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

    public void Add(TParameterizedType element)
    {
        if (LimitCount > 0 && Value.Count == LimitCount)
        {
            OnError($"Текущее количество элементов достигло предела в {LimitCount} элемента(ов)");
            return;
        }
        Value.Add(element);

        OnChange(Value);
    }

    public void Add(object element)
    {
        if (element is TParameterizedType pType)
            Add(pType);
    }

    public void Remove(TParameterizedType element)
    {
        if (!Value.Contains(element)) return;

        Value.Remove(element);

        OnChange(Value);
    }

    public void Remove(object element)
    {
        if (element is TParameterizedType pType)
            Remove(pType);
    }

    public void Clear()
    {
        Value = new List<TParameterizedType>();

        OnChange(Value);
    }

    protected void OnError(string message)
    {
        Error?.Invoke(Info.Name, message);
    }

    protected void OnChange(object value)
    {
        Change?.Invoke(Info.Name, value);
    }

    object ICollectionParameter.this[int index]
    {
        get => this[index]!;
        set => this[index] = (TParameterizedType)value;
    }

    public TParameterizedType this[int index]
    {
        get
        {
            if(index < 0 || index >= Value.Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            return Value.ToArray()[index];
        }
        set
        {
            if (index < 0 || index >= Value.Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            Value.ToArray()[index] = value;
        }
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