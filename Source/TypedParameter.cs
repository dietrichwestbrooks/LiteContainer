using System.Collections.Generic;

namespace Wayne.Payment.Platform.Lite
{
    public class TypedParameter<TValue> : ResolveParameter
    {
        private TValue _value;

        public TypedParameter(TValue value)
        {
            _value = value;
        }

        internal override void AddPolicies(IList<IBuilderPolicy> policies)
        {
            policies.Add(new TypedParameterPolicy(typeof(TValue), _value));
        }
    }
}
