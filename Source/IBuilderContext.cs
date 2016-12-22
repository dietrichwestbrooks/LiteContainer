using System.Collections.Generic;

<<<<<<< HEAD
namespace Wayne.Payment.Platform.Lite
=======
namespace Wayne.Payment.Platform
>>>>>>> 06f38426eb2a120e3f5be0a79f2c3cf88f9ff4e4
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
