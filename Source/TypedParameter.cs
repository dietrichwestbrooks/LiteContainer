using System;
using System.Collections.Generic;

namespace Wayne.Payment.Platform
{
    public class TypedParameter : ResolveParameter
    {
        private Type _serviceType;
        private object _serviceValue;

        public TypedParameter(Type serviceType, object serviceValue)
        {
            _serviceType = serviceType;
            _serviceValue = serviceValue;
        }

        internal override void AddPolicies(IList<IBuilderPolicy> policies)
        {
            policies.Add(new TypedParameterPolicy(_serviceType, _serviceValue));
        }
    }

    public class TypedParameter<TService> : TypedParameter
    {
        public TypedParameter(TService serviceValue)
            : base(typeof(TService), serviceValue)
        {
        }
    }
}
