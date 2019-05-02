using System;

namespace LiteContainer
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