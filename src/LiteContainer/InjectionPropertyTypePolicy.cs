using System;
using System.Linq;
using System.Reflection;

namespace LiteContainer
{
    internal class InjectionPropertyTypePolicy : IInjectionPolicy
    {
        private Type _propType;

        public InjectionPropertyTypePolicy(Type propType)
        {
            _propType = propType;
        }

        public void Inject(IBuilderContext context)
        {
            object value = ResolveParameterExtensions.Resolve(context, _propType);

            var properties = context.Current.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => _propType == p.PropertyType);

            foreach (var property in properties)
            {
                try
                {
                    property.SetValue(context.Current, value, null);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException(string.Format("Unable to set value for property:{0}", property.Name), ex);
                } 
            }
        }
    }
}