<<<<<<< HEAD
﻿using System.Collections.Generic;

namespace Wayne.Payment.Platform.Lite
=======
﻿using System;
using System.Collections.Generic;

namespace Wayne.Payment.Platform
>>>>>>> 06f38426eb2a120e3f5be0a79f2c3cf88f9ff4e4
{
    internal interface ILifetimeContainer : IEnumerable<object>
    {
        void Add(object item);

        void Remove(object item);

        bool Contains(object item);

        int Count { get; }
    }
}
