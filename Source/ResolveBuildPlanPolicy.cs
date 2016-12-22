using System.Linq;

<<<<<<< HEAD
namespace Wayne.Payment.Platform.Lite
=======
namespace Wayne.Payment.Platform
>>>>>>> 06f38426eb2a120e3f5be0a79f2c3cf88f9ff4e4
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
