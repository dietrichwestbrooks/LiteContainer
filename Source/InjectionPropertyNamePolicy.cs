using System;
<<<<<<< HEAD
using System.Reflection;

namespace Wayne.Payment.Platform.Lite
=======

namespace Wayne.Payment.Platform
>>>>>>> 06f38426eb2a120e3f5be0a79f2c3cf88f9ff4e4
{
    internal class InjectionPropertyNamePolicy : IInjectionPolicy
    {
        private string _propName;

        public InjectionPropertyNamePolicy(string propName)
        {
            _propName = propName;
        }

        public void Inject(IBuilderContext context)
        {
<<<<<<< HEAD
            var property = context.Current.GetType().GetProperty(_propName, BindingFlags.Instance | BindingFlags.Public);
=======
            var property = context.Current.GetType().GetProperty(_propName);
>>>>>>> 06f38426eb2a120e3f5be0a79f2c3cf88f9ff4e4

            if (property == null)
                throw new InvalidOperationException(string.Format("Missing property:{0}", _propName));

<<<<<<< HEAD
            try
            {
                object value = ResolveParameterExtensions.Resolve(context, property.PropertyType);
                property.SetValue(context.Current, value, null);
=======
            Type propType = property.PropertyType;

            try
            {
                object propValue = ResolveParameterExtensions.Resolve(context, propType);
                property.SetValue(context.Current, propValue, null);
>>>>>>> 06f38426eb2a120e3f5be0a79f2c3cf88f9ff4e4
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(string.Format("Unable to resolve instance for property:{0}", property.Name), ex);
            }
        }
    }
}
