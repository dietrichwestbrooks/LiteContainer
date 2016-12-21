namespace Wayne.Payment.Platform
{
    public class ResolveParameters
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

        public T DependencyParameter<T>()
        {
            return (T)_context.NewBuildUp(BuildKey.Create(typeof(T)));
        }

        public T DependencyParameter<T>(string name)
        {
            return (T)_context.NewBuildUp(BuildKey.Create(typeof(T), name));
        }
    }
}
