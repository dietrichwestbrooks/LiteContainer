namespace Wayne.Payment.Platform
{
    internal interface IConstructorPolicy : IBuilderPolicy
    {
        object Construct(IBuilderContext context);
    }
}
