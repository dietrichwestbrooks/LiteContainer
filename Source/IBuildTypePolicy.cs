using System;

namespace Wayne.Payment.Platform
{
    internal interface IBuildTypePolicy : IBuilderPolicy
    {
         Type Type { get; }
    }
}
