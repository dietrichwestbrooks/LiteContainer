using System;
using System.Linq;

namespace Wayne.Payment.Platform
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
            var properties = context.Current.GetType().GetProperties().Where(p => _propType == p.PropertyType);

            foreach (var property in properties)
            {
                Type propType = property.PropertyType;

                try
                {
                    object propValue = ResolveParameterExtensions.Resolve(context, propType);
                    property.SetValue(context.Current, propValue, null);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException(string.Format("Unable to resolve instance for property:{0}", property.Name), ex);
                } 
            }
        }
    }
}