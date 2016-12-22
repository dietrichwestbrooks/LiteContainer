using System;
using System.Linq;
<<<<<<< HEAD
using System.Reflection;

namespace Wayne.Payment.Platform.Lite
=======

namespace Wayne.Payment.Platform
>>>>>>> 06f38426eb2a120e3f5be0a79f2c3cf88f9ff4e4
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
<<<<<<< HEAD
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
=======
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
>>>>>>> 06f38426eb2a120e3f5be0a79f2c3cf88f9ff4e4
                } 
            }
        }
    }
}