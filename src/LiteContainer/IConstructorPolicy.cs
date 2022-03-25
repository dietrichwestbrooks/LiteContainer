namespace LiteContainer
{
    internal interface IConstructorPolicy : IBuilderPolicy
    {
        object Construct(IBuilderContext context);
    }
}
