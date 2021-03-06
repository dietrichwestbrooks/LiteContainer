﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace LiteContainer
{
    internal class InjectionConstructorPolicy : IConstructorPolicy
    {
        private IEnumerable<Type> _parameterTypes;

        public InjectionConstructorPolicy(IEnumerable<Type> parameterTypes)
        {
            _parameterTypes = parameterTypes;
        }

        public object Construct(IBuilderContext context)
        {
            var buildTypePolicy = context.Policies.Single(p => p is IBuildTypePolicy) as IBuildTypePolicy;

            if (buildTypePolicy == null)
                throw new InvalidOperationException("No build type policy found");

            var buildType = buildTypePolicy.Type;

            var constructor = buildType.GetConstructors()
                .FirstOrDefault(c => c.GetParameters().Select(p => p.ParameterType).SequenceEqual(_parameterTypes));

            if (constructor == null)
                throw new InvalidOperationException("No suitable constructor found");

            var parameters = constructor.GetParameters()
                .Select(p => ResolveParameterExtensions.Resolve(context, p))
                .ToArray();

            return constructor.Invoke(parameters);
        }
    }
}
