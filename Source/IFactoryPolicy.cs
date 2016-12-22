namespace Wayne.Payment.Platform.Lite
{
    internal interface IFactoryPolicy : IBuilderPolicy
    {
        object Create(IBuilderContext context);
    }
}
