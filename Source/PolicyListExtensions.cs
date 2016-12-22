using System.Collections.Generic;
using System.Linq;

namespace Wayne.Payment.Platform.Lite
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
