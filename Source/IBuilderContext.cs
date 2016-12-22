using System.Collections.Generic;

namespace Wayne.Payment.Platform.Lite
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
