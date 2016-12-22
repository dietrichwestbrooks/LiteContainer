using System.Collections.Generic;

namespace Wayne.Payment.Platform.Lite
{
    public abstract class ResolveParameter
    {
        internal abstract void AddPolicies(IList<IBuilderPolicy> policies);
    }
}
