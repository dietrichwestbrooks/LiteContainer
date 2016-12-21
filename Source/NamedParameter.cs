using System.Collections.Generic;

namespace Wayne.Payment.Platform
{
    public class NamedParameter : ResolveParameter
    {
        private string _paramName;
        private object _paramValue;

        public NamedParameter(string paramName, object paramValue)
        {
            _paramName = paramName;
            _paramValue = paramValue;
        }

        internal override void AddPolicies(IList<IBuilderPolicy> policies)
        {
            policies.Add(new NamedParameterPolicy(_paramName, _paramValue));
        }
    }
}
