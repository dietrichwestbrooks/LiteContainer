namespace Wayne.Payment.Platform.Lite
{
    internal sealed class TransientLifetimePolicy : ILifetimePolicy
    {
        public object GetValue()
        {
            return null;
        }

        public void SetValue(object newValue)
        {
        }

        public void RemoveValue()
        {
        }
    }
}