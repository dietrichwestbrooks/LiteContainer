<<<<<<< HEAD
﻿namespace Wayne.Payment.Platform.Lite
=======
﻿namespace Wayne.Payment.Platform
>>>>>>> 06f38426eb2a120e3f5be0a79f2c3cf88f9ff4e4
{
    internal interface ILifetimePolicy : IBuilderPolicy
    {
        object GetValue();

        void SetValue(object newValue);

        void RemoveValue();
    }
}
