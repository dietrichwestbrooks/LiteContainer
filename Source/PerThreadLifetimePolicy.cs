using System;
using System.Collections.Generic;
using System.Threading;

namespace Wayne.Payment.Platform.Lite
{
    public sealed class PerThreadLifetimePolicy : ILifetimePolicy
    {
        private const string DataSlotName = "Lifetime";
        private Guid _key;

        public PerThreadLifetimePolicy()
        {
            _key = Guid.NewGuid();
        }

        public object GetValue()
        {
            var values = GetValues();

            object value;

            values.TryGetValue(_key, out value);

            return value;
        }

        public void SetValue(object newValue)
        {
            var values = GetValues();
            values[_key] = newValue;
        }

        public void RemoveValue()
        {
        }

        private Dictionary<Guid, object> GetValues()
        {
            var values = (Dictionary<Guid, object>)Thread.GetData(Thread.GetNamedDataSlot(DataSlotName));

            if (values != null)
                return values;

            values = new Dictionary<Guid, object>();

            Thread.SetData(Thread.GetNamedDataSlot(DataSlotName), values);

            return values;
        }
    }
}