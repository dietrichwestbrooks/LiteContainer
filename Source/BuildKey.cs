using System;

<<<<<<< HEAD
namespace Wayne.Payment.Platform.Lite
{
    internal sealed class BuildKey
=======
namespace Wayne.Payment.Platform
{
    internal class BuildKey
>>>>>>> 06f38426eb2a120e3f5be0a79f2c3cf88f9ff4e4
    {
        public BuildKey(Type type)
        {
            Type = type;
            Name = string.Empty;
        }

        internal static BuildKey Create(Type type)
        {
            return new BuildKey(type);
        }

        internal static BuildKey Create(Type type, string name)
        {
            return new BuildKey(type, name);
        }

        internal static BuildKey Create(BuildKey key)
        {
            return new BuildKey(key.Type, key.Name);
        }

        public BuildKey(Type type, string name)
        {
            Type = type;
            Name = name;
        }

        public Type Type { get; set; }

        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            var key = obj as BuildKey;

            if (key == null)
                return false;

            return Type == key.Type && Name.Equals(key.Name);
        }

        public override int GetHashCode()
        {
            return string.Format("{0}-{1}", Type, Name).GetHashCode();
        }

        public override string ToString()
        {
            return !string.IsNullOrEmpty(Name) ? string.Format("{0}[{1}]", Type.Name, Name) : Type.Name;
        }
    }
}
