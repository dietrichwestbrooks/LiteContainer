<<<<<<< HEAD
﻿using System.Linq;

namespace Wayne.Payment.Platform.Lite
=======
﻿using System;
using System.Linq;

namespace Wayne.Payment.Platform
>>>>>>> 06f38426eb2a120e3f5be0a79f2c3cf88f9ff4e4
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