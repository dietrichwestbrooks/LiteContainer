<<<<<<< HEAD
﻿namespace Wayne.Payment.Platform.Lite
=======
﻿namespace Wayne.Payment.Platform
>>>>>>> 06f38426eb2a120e3f5be0a79f2c3cf88f9ff4e4
{
    internal class NamedParameterPolicy : IResolveParameterPolicy
    {
        public NamedParameterPolicy(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; private set; }
        public object Value { get; private set; }
    }
}
