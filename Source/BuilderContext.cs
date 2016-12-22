using System.Collections.Generic;
using System.Linq;

<<<<<<< HEAD
namespace Wayne.Payment.Platform.Lite
{
    internal sealed class BuilderContext : IBuilderContext
=======
namespace Wayne.Payment.Platform
{
    internal class BuilderContext : IBuilderContext
>>>>>>> 06f38426eb2a120e3f5be0a79f2c3cf88f9ff4e4
    {
        public BuilderContext(LiteContainer container, BuildKey key, IList<IBuilderPolicy> policies, LifetimeContainer lifetime)
        {
            Container = container;
            BuildKey = key;
            Policies = policies;
            Lifetime = lifetime;
        }

        private LiteContainer Container { get; set; }

        public BuildKey BuildKey { get; private set; }

        public object Current { get; set; }

        public ILifetimeContainer Lifetime { get; private set; }

        public IList<IBuilderPolicy> Policies { get; private set; }

        public bool BuildComplete { get; set; }

        public object NewBuildUp(BuildKey key)
        {
            return Container.NewBuildUp(key, Policies.OfType<IResolveParameterPolicy>().ToArray());
        }
    }
}
