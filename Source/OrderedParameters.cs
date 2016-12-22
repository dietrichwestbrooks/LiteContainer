using System.Collections.Generic;

namespace Wayne.Payment.Platform.Lite
{
    public class OrderedParameters : ResolveParameter
    {
        private object[] _parameters;

        public OrderedParameters(params object[] parameters)
        {
            _parameters = parameters;
        }

        internal override void AddPolicies(IList<IBuilderPolicy> policies)
        {
            policies.Add(new OrderedParametersPolicy(_parameters));
        }
    }
}
