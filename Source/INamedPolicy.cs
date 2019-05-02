namespace LiteContainer
{
    internal interface INamedPolicy : IBuilderPolicy
    {
         string Name { get; }
    }
}
