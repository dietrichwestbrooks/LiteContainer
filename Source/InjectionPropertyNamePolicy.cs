using System;

namespace Wayne.Payment.Platform
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
            var property = context.Current.GetType().GetProperty(_propName);

            if (property == null)
                throw new InvalidOperationException(string.Format("Missing property:{0}", _propName));

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
