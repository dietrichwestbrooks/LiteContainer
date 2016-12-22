using System.Collections.Generic;
using System.Linq;

<<<<<<< HEAD
namespace Wayne.Payment.Platform.Lite
=======
namespace Wayne.Payment.Platform
>>>>>>> 06f38426eb2a120e3f5be0a79f2c3cf88f9ff4e4
{
    internal static class PolicyListExtensions
    {
        public static void AddPolicies(this IList<IBuilderPolicy> source, params IBuilderPolicy[] policies)
        {
            foreach (var policy in policies)
            {
                source.Add(policy);
            }
        }

        public static void Set<TPolicy>(this IList<IBuilderPolicy> source, TPolicy newPolicy)
            where TPolicy : IBuilderPolicy
        {
            var oldPolicy = source.Single(p => p is TPolicy);

            if (oldPolicy != null)
            {
                source.Remove(oldPolicy);
            }

            source.Add(newPolicy);
        }

        public static void Set(this IList<IBuilderPolicy> source, ILifetimePolicy newLifetimePolicy)
        {
            var oldLifetimeolicy = source.OfType<ILifetimePolicy>().Single();

            if (oldLifetimeolicy != null)
            {
                newLifetimePolicy.SetValue(oldLifetimeolicy.GetValue());
                source.Remove(oldLifetimeolicy);
            }

            source.Add(newLifetimePolicy);
        }
    }
}
