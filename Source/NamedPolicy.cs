<<<<<<< HEAD
﻿namespace Wayne.Payment.Platform.Lite
=======
﻿namespace Wayne.Payment.Platform
>>>>>>> 06f38426eb2a120e3f5be0a79f2c3cf88f9ff4e4
{
    internal class NamedPolicy : INamedPolicy
    {
        public NamedPolicy()
        {
            Name = string.Empty;
        }

        public NamedPolicy(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}
