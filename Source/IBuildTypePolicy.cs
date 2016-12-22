using System;

namespace Wayne.Payment.Platform.Lite
{
    internal interface IBuildTypePolicy : IBuilderPolicy
    {
         Type Type { get; }
    }
}
