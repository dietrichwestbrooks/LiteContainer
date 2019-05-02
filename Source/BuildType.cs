using System;

namespace LiteContainer
{
    internal sealed class BuildType : IBuildTypePolicy
    {
        public BuildType(Type type)
        {
            Type = type;
        }

        public Type Type { get; private set; }
    }
}
