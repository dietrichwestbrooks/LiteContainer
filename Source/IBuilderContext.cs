using System.Collections.Generic;

namespace Wayne.Payment.Platform
{
    internal interface IBuilderContext
    {
        BuildKey BuildKey { get; }

        ILifetimeContainer Lifetime { get; }

        IList<IBuilderPolicy> Policies { get; }

        bool BuildComplete { get; set; }

        object Current { get; set; }

        object NewBuildUp(BuildKey key);
    }
}
