using System.Collections.Generic;

namespace LiteContainer
{
    internal interface ILifetimeContainer : IEnumerable<object>
    {
        void Add(object item);

        void Remove(object item);

        bool Contains(object item);

        int Count { get; }
    }
}
