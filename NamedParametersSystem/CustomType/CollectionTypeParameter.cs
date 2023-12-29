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
            if (LimitCount > 0 && value.Count > LimitCount)
            {
                OnError($"Количество элементов в списке превышает допустимое в {LimitCount} шт");
                return;
            }

            var b = val is null || !val.Equals(value);

            val = value;

            if(b) OnChange(val);
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
        Info.LimitCount = info.LimitCount;

        OnChange(Value);
    }

    public override int LimitCount => Info.LimitCount;

    public override IParameter ElementParameter => 
        new CustomTypeParameter<TParameterizedType>(Info.ElementInfo);
}