<<<<<<< HEAD
﻿using System.Linq;

namespace Wayne.Payment.Platform.Lite
{
    public sealed class ResolveParameters
=======
﻿namespace Wayne.Payment.Platform
{
    public class ResolveParameters
>>>>>>> 06f38426eb2a120e3f5be0a79f2c3cf88f9ff4e4
    {
        private IBuilderContext _context;

        internal ResolveParameters(IBuilderContext context)
        {
            _context = context;
        }

        public T NamedParameter<T>(string name)
        {
            return ResolveParameterExtensions.Resolve<T>(_context, name);
        }

        public T TypedParameter<T>()
        {
            return ResolveParameterExtensions.Resolve<T>(_context);
        }

<<<<<<< HEAD
        public object[] OrderedParameters()
        {
            return _context.Policies.OfType<OrderedParametersPolicy>().Single().Parameters;
=======
        public T DependencyParameter<T>()
        {
            return (T)_context.NewBuildUp(BuildKey.Create(typeof(T)));
        }

        public T DependencyParameter<T>(string name)
        {
            return (T)_context.NewBuildUp(BuildKey.Create(typeof(T), name));
>>>>>>> 06f38426eb2a120e3f5be0a79f2c3cf88f9ff4e4
        }
    }
}
