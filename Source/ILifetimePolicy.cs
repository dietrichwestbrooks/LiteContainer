namespace Wayne.Payment.Platform
{
    internal interface ILifetimePolicy : IBuilderPolicy
    {
        object GetValue();

        void SetValue(object newValue);

        void RemoveValue();
    }
}
