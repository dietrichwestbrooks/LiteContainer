namespace Wayne.Payment.Platform
{
    internal interface IInjectionPolicy : IBuilderPolicy
    {
        void Inject(IBuilderContext context);
    }
}
