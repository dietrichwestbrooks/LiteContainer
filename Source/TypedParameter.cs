<<<<<<< HEAD
﻿using System.Collections.Generic;

namespace Wayne.Payment.Platform.Lite
{
    public class TypedParameter<TValue> : ResolveParameter
    {
        private TValue _value;

        public TypedParameter(TValue value)
        {
            _value = value;
=======
﻿using System;
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
>>>>>>> 06f38426eb2a120e3f5be0a79f2c3cf88f9ff4e4
        }

        internal override void AddPolicies(IList<IBuilderPolicy> policies)
        {
<<<<<<< HEAD
            policies.Add(new TypedParameterPolicy(typeof(TValue), _value));
=======
            policies.Add(new TypedParameterPolicy(_serviceType, _serviceValue));
        }
    }

    public class TypedParameter<TService> : TypedParameter
    {
        public TypedParameter(TService serviceValue)
            : base(typeof(TService), serviceValue)
        {
>>>>>>> 06f38426eb2a120e3f5be0a79f2c3cf88f9ff4e4
        }
    }
}
