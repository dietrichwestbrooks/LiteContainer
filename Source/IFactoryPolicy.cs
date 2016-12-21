namespace Wayne.Payment.Platform
{
    internal interface IFactoryPolicy : IBuilderPolicy
    {
        object Create(IBuilderContext context);
    }
}
