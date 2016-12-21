using System;
using System.Collections.Generic;

namespace Wayne.Payment.Platform
{
    internal interface ILifetimeContainer : IEnumerable<object>
    {
        void Add(object item);

        void Remove(object item);

        bool Contains(object item);

        int Count { get; }
    }
}
