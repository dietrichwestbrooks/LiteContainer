using System;

namespace Wayne.Payment.Platform
{
    internal sealed class ExternalLifetimePolicy : ILifetimePolicy
    {
        private WeakReference Value { get; set; }

        public object GetValue()
        {
            return Value != null ? Value.Target : null;
        }

        public void SetValue(object newValue)
        {
            Value = new WeakReference(newValue, false);
        }

        public void RemoveValue()
        {
            Value = null;
        }
    }
}
