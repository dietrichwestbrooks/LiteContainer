namespace Wayne.Payment.Platform.Lite
{
    internal interface IInjectionPolicy : IBuilderPolicy
    {
        void Inject(IBuilderContext context);
    }
}
