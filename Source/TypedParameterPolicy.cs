﻿using System;

namespace LiteContainer
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
