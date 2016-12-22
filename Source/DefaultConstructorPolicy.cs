using System;
using System.Linq;

namespace Wayne.Payment.Platform.Lite
{
    internal sealed class DefaultConstructorPolicy : IConstructorPolicy
    {
        public object Construct(IBuilderContext context)
        {
            var buildTypePolicy = context.Policies.Single(p => p is IBuildTypePolicy) as IBuildTypePolicy;

            if (buildTypePolicy == null)
                throw new InvalidOperationException("No build type policy found");

            return buildTypePolicy.Type.GetConstructor(new Type[] { }).Invoke(null);
        }
    }
}
