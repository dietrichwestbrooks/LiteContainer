﻿using System;

namespace Wayne.Payment.Platform
{
    internal class BuildType : IBuildTypePolicy
    {
        public BuildType(Type type)
        {
            Type = type;
        }

        public Type Type { get; private set; }
    }
}
