namespace Wayne.Payment.Platform
{
    internal interface INamedPolicy : IBuilderPolicy
    {
         string Name { get; }
    }
}
