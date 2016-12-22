using System.Collections.Generic;

namespace Wayne.Payment.Platform.Lite
{
    public class NamedParameter<TValue> : ResolveParameter
    {
        private string _name;
        private TValue _value;

        public NamedParameter(string name, TValue value)
        {
            _name = name;
            _value = value;
        }

        internal override void AddPolicies(IList<IBuilderPolicy> policies)
        {
            policies.Add(new NamedParameterPolicy(_name, _value));
        }
    }
}
