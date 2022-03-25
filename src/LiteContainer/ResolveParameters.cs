using System.Linq;

namespace LiteContainer
{
    public sealed class ResolveParameters
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

        public object[] OrderedParameters()
        {
            return _context.Policies.OfType<OrderedParametersPolicy>().Single().Parameters;
        }
    }
}
