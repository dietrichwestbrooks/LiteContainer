using System;

<<<<<<< HEAD
namespace Wayne.Payment.Platform.Lite
=======
namespace Wayne.Payment.Platform
>>>>>>> 06f38426eb2a120e3f5be0a79f2c3cf88f9ff4e4
{
    internal class TypedParameterPolicy : IResolveParameterPolicy
    {
        public TypedParameterPolicy(Type type, object value)
        {
            Type = type;
            Value = value;
        }

        public Type Type { get; private set; }
        public object Value { get; private set; }
    }
}
