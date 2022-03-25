using System;
using System.Reflection;

namespace LiteContainer
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
            var property = context.Current.GetType().GetProperty(_propName, BindingFlags.Instance | BindingFlags.Public);

            if (property == null)
                throw new InvalidOperationException(string.Format("Missing property:{0}", _propName));

            try
            {
                object value = ResolveParameterExtensions.Resolve(context, property.PropertyType);
                property.SetValue(context.Current, value, null);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(string.Format("Unable to resolve instance for property:{0}", property.Name), ex);
            }
        }
    }
}
