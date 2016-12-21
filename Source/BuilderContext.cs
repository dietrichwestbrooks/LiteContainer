using System.Collections.Generic;
using System.Linq;

namespace Wayne.Payment.Platform
{
    internal class BuilderContext : IBuilderContext
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
