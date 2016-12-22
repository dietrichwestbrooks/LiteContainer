namespace Wayne.Payment.Platform.Lite
{
    internal interface ILifetimePolicy : IBuilderPolicy
    {
        object GetValue();

        void SetValue(object newValue);

        void RemoveValue();
    }
}
