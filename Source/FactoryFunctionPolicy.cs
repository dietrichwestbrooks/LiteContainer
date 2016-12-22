using System;
<<<<<<< HEAD
using System.Collections.Generic;
using System.Linq;

namespace Wayne.Payment.Platform.Lite
=======
using System.Linq;

namespace Wayne.Payment.Platform
>>>>>>> 06f38426eb2a120e3f5be0a79f2c3cf88f9ff4e4
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

<<<<<<< HEAD
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
=======
            var parameters = method.GetParameters().Select(p => ResolveParameterExtensions.Resolve(context, p));
>>>>>>> 06f38426eb2a120e3f5be0a79f2c3cf88f9ff4e4

            return method.Invoke(factory, parameters.ToArray());
        }
    }
}
