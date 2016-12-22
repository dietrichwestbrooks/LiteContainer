using System;

<<<<<<< HEAD
namespace Wayne.Payment.Platform.Lite
{
    internal sealed class BuildType : IBuildTypePolicy
=======
namespace Wayne.Payment.Platform
{
    internal class BuildType : IBuildTypePolicy
>>>>>>> 06f38426eb2a120e3f5be0a79f2c3cf88f9ff4e4
    {
        public BuildType(Type type)
        {
            Type = type;
        }

        public Type Type { get; private set; }
    }
}
