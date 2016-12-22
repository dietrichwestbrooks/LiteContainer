namespace Wayne.Payment.Platform.Lite
{
    internal interface IConstructorPolicy : IBuilderPolicy
    {
        object Construct(IBuilderContext context);
    }
}
