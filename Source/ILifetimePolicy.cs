﻿namespace LiteContainer
{
    internal interface ILifetimePolicy : IBuilderPolicy
    {
        object GetValue();

        void SetValue(object newValue);

        void RemoveValue();
    }
}
