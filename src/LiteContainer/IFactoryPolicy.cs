namespace LiteContainer
{
    internal interface IFactoryPolicy : IBuilderPolicy
    {
        object Create(IBuilderContext context);
    }
}
