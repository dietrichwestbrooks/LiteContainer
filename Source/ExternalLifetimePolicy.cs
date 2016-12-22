using System;

<<<<<<< HEAD
namespace Wayne.Payment.Platform.Lite
=======
namespace Wayne.Payment.Platform
>>>>>>> 06f38426eb2a120e3f5be0a79f2c3cf88f9ff4e4
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
