using System;
using System.Linq;

<<<<<<< HEAD
namespace Wayne.Payment.Platform.Lite
{
    internal sealed class DefaultConstructorPolicy : IConstructorPolicy
=======
namespace Wayne.Payment.Platform
{
    internal class DefaultConstructorPolicy : IConstructorPolicy
>>>>>>> 06f38426eb2a120e3f5be0a79f2c3cf88f9ff4e4
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
