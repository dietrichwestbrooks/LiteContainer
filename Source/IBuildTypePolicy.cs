using System;

namespace LiteContainer
{
    internal interface IBuildTypePolicy : IBuilderPolicy
    {
         Type Type { get; }
    }
}
