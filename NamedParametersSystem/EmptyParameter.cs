namespace NamedParametersSystem;

public class EmptyParameter : ParametrizedType
{
    public string Name => nameParameter.Value;
    public string Value => valueParameter.Value;

    private readonly StringParameter nameParameter = 
        new StringParameter(new CustomParameterInfo<string>("Пустой параметр")
    {
        Name = "Имя",
        Description = "Имя пустого параметра",
        ReadOnly = true
    });
    private readonly StringParameter valueParameter = 
        new StringParameter(new CustomParameterInfo<string>("NaN")
    {
        Name = "Значение",
        Description = "Значение пустого параметра",
        ReadOnly = true
    });
}