namespace LiteContainer
{
    internal interface IInjectionPolicy : IBuilderPolicy
    {
        void Inject(IBuilderContext context);
    }
}
