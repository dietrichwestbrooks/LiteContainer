using System;
using System.Collections.Generic;
using System.Linq;

namespace Wayne.Payment.Platform.Lite
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

            var parameters = new List<object>();

            var parameter = method.GetParameters().FirstOrDefault();

            if (parameter != null)
            {
                var resolvedParameters = new ResolveParameters(context);

                if (parameter.ParameterType == typeof (ResolveParameters))
                {
                    parameters.Add(resolvedParameters);
                }
                else if (parameter.ParameterType.IsArray && parameter.ParameterType.GetElementType() == typeof (object))
                {
                    parameters.Add(resolvedParameters.OrderedParameters());
                }
                else
                {
                    throw new InvalidOperationException(string.Format("Invalid argument type:{0}",
                        parameter.ParameterType.Name));
                }
            }

            return method.Invoke(factory, parameters.ToArray());
        }
    }
}
