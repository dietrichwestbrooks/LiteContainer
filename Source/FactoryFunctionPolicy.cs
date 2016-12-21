using System;
using System.Linq;

namespace Wayne.Payment.Platform
{
    internal class FactoryFunctionPolicy : IFactoryPolicy
    {
        private BuildKey _factoryKey;

        public FactoryFunctionPolicy(BuildKey factoryKey)
        {
            _factoryKey = factoryKey;
        }

        public object Create(IBuilderContext context)
        {
            var factory = (Delegate)context.NewBuildUp(_factoryKey);

            if (factory == null)
                throw new InvalidOperationException("Unable to resolve factory delegate");

            var method = factory.GetType().GetMethod("Invoke");

            var parameters = method.GetParameters().Select(p => ResolveParameterExtensions.Resolve(context, p));

            return method.Invoke(factory, parameters.ToArray());
        }
    }
}
