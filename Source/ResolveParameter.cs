using System.Collections.Generic;

namespace LiteContainer
{
    public abstract class ResolveParameter
    {
        internal abstract void AddPolicies(IList<IBuilderPolicy> policies);
    }
}
