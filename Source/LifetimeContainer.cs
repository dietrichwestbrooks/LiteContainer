using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Wayne.Payment.Platform
{
    internal class LifetimeContainer : ILifetimeContainer, IDisposable
    {
        private bool _disposed;
        private object _syncRoot = new object();
        private List<object> _disposables = new List<object>(); 

        ~LifetimeContainer()
        {
            Dispose(false);
        }

        public void Add(object item)
        {
            _disposables.Add(item);
        }

        public bool Contains(object item)
        {
            return _disposables.Contains(item);
        }

        public void Remove(object item)
        {
            _disposables.Remove(item);
        }

        public int Count
        {
            get { return _disposables.Count; }
        }

        public IEnumerator<object> GetEnumerator()
        {
            return _disposables.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            lock (_syncRoot)
            {
                if (_disposed)
                    return;

                if (disposing)
                {
                    foreach (var disposable in _disposables.OfType<IDisposable>())
                    {
                        disposable.Dispose();
                    }

                    _disposables.Clear();
                }

                _disposed = true; 
            }
        }
    }
}
