namespace Wayne.Payment.Platform.Lite
{
    internal interface INamedPolicy : IBuilderPolicy
    {
         string Name { get; }
    }
}
