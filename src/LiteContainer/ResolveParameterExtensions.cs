using System;
using System.Linq;
using System.Reflection;

namespace LiteContainer
{
    internal static class ResolveParameterExtensions
    {
        public static object Resolve(IBuilderContext context, ParameterInfo parameter)
        {
            var type = parameter.ParameterType;
            var name = parameter.Name;

            var resolveParameters = context.Policies.OfType<IResolveParameterPolicy>().ToArray();

            foreach (var resolveParameter in resolveParameters.OfType<NamedParameterPolicy>())
            {
                if (name == resolveParameter.Name)
                {
                    return resolveParameter.Value;
                }
            }

            foreach (var resolveParameter in resolveParameters.OfType<TypedParameterPolicy>())
            {
                if (type == resolveParameter.Type)
                {
                    return resolveParameter.Value;
                }
            }

            if (type.IsArray && type.GetElementType().IsInterface)
            {
                return context.NewBuildUp(BuildKey.Create(type));
            }
            
            if (type.IsInterface)
            {
                var paramValue = context.NewBuildUp(BuildKey.Create(type));

                if (paramValue != null)
                    return paramValue;
            }

            throw new InvalidOperationException(string.Format("Unable to resolve parameter:{0}", name));
        }

        public static T Resolve<T>(IBuilderContext context, string name)
        {
            var resolveParameters = context.Policies.OfType<IResolveParameterPolicy>().ToArray();

            foreach (var resolveParameter in resolveParameters.OfType<NamedParameterPolicy>())
            {
                if (name == resolveParameter.Name)
                {
                    return (T)resolveParameter.Value;
                }
            }

            throw new InvalidOperationException(string.Format("Unable to resolve parameter:{0}", name));
        }

        public static T Resolve<T>(IBuilderContext context)
        {
            return (T) Resolve(context, typeof (T));
        }

        public static object Resolve(IBuilderContext context, Type type)
        {
            var resolveParameters = context.Policies.OfType<IResolveParameterPolicy>().ToArray();

            foreach (var resolveParameter in resolveParameters.OfType<TypedParameterPolicy>())
            {
                if (type == resolveParameter.Type)
                {
                    return resolveParameter.Value;
                }
            }

            throw new InvalidOperationException(string.Format("Unable to resolve parameter type:{0}", type.Name));
        }
    }
}
