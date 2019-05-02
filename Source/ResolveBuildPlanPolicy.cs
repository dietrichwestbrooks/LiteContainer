using System.Linq;

namespace LiteContainer
{
    internal class ResolveBuildPlanPolicy : IBuildPlanPolicy
    {
        public object BuildUp(IBuilderContext context)
        {
            var type = context.Policies.OfType<IBuildTypePolicy>().Single().Type;

            var name = context.Policies.OfType<INamedPolicy>().Single().Name;

            object service = context.NewBuildUp(BuildKey.Create(type, name));

            if (service == null)
                return null;

            context.Current = service;

            context.BuildComplete = true;

            return service;
        }
    }
}
