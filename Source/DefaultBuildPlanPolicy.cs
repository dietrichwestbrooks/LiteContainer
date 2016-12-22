using System.Linq;

namespace Wayne.Payment.Platform.Lite
{
    internal class DefaultBuildPlanPolicy : IBuildPlanPolicy
    {
        public object BuildUp(IBuilderContext context)
        {
            var constructPolicy = context.Policies.OfType<IConstructorPolicy>().Single();

            object service = constructPolicy.Construct(context);

            if (service == null)
                return null;

            context.Current = service;

            foreach (var injectPolicy in context.Policies.OfType<IInjectionPolicy>())
            {
                injectPolicy.Inject(context);
            }

            context.BuildComplete = true;

            return service;
        }
    }
}