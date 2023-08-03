namespace NamedParametersSystem;

public sealed class CollectionTypeParameter<TParameterizedType> : 
    CollectionParameter<TParameterizedType, CollectionParameterInfo<TParameterizedType>>,
    IParameter, ICollectionParameter
    where TParameterizedType : IParameterizedType
{
    private ICollection<TParameterizedType>? val;

    public override CollectionParameterInfo<TParameterizedType> Info { get; }

    public override ICollection<TParameterizedType> Value
    {
        get => val ?? Info.DefaultValue;
        set
        {
            if (value.Count > LimitCount)
            {
                OnError($"Количество элементов в списке превышает допустимое в {LimitCount} шт");
                return;
            }
            if(val is null || !val.Equals(value))
                OnChange(value);

            val = value;
        } 

    }

    public CollectionTypeParameter(CollectionParameterInfo<TParameterizedType> info, IEnumerable<TParameterizedType>? value = default)
    {
        Info = info;
        Value = value?.ToList() ?? Info.DefaultValue;
    }

    public CollectionTypeParameter()
        : this(new CollectionParameterInfo<TParameterizedType>()) { }

    public override void ChangeInfo(CollectionParameterInfo<TParameterizedType> info)
    {
        Info.ReadOnly = info.ReadOnly;
        Info.DefaultValue = info.DefaultValue;
        Info.ElementInfo = info.ElementInfo;
    }

    public int LimitCount => Info.LimitCount;

    public void Add(TParameterizedType element)
    {
        if (Value.Count == LimitCount)
        {
            OnError($"Текущее количество элементов достигло предела в {LimitCount} элементов");
            return;
        }
        Value.Add(element);
    }

    public IParameter ElementParameter => 
        new CustomTypeParameter<TParameterizedType>(Info.ElementInfo);
}

