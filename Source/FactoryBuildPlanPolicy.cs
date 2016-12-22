using System.Linq;

<<<<<<< HEAD
namespace Wayne.Payment.Platform.Lite
=======
namespace Wayne.Payment.Platform
>>>>>>> 06f38426eb2a120e3f5be0a79f2c3cf88f9ff4e4
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
