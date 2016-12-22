using System;

<<<<<<< HEAD
namespace Wayne.Payment.Platform.Lite
=======
namespace Wayne.Payment.Platform
>>>>>>> 06f38426eb2a120e3f5be0a79f2c3cf88f9ff4e4
{
    internal sealed class SingletonLifetimePolicy : ILifetimePolicy, IDisposable
    {
        private bool _disposed;

        public SingletonLifetimePolicy()
        {
        }

        public SingletonLifetimePolicy(object service)
        {
            SetValue(service);
        }

        ~SingletonLifetimePolicy()
        {
            Dispose(false);
        }

        private object Value { get; set; }

        public object GetValue()
        {
            return Value;
        }

        public void SetValue(object newValue)
        {
            Value = newValue;
        }

        public void RemoveValue()
        {
            Value = null;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                var disposable = Value as IDisposable;

                if (disposable != null)
                    disposable.Dispose();
            }

            _disposed = true;
        }
    }
}