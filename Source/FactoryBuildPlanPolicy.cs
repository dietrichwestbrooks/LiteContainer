using System.Linq;

namespace LiteContainer
{
    internal class FactoryBuildPlanPolicy : IBuildPlanPolicy
    {
        public object BuildUp(IBuilderContext context)
        {
            var factoryPolicy = context.Policies.OfType<IFactoryPolicy>().Single();

            var service = factoryPolicy.Create(context);

            if (service == null)
                return null;

            context.Current = service;

            context.BuildComplete = true;

            return service;
        }
    }
}
