using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

<<<<<<< HEAD
namespace Wayne.Payment.Platform.Lite
{
    public sealed class ContainerRegistration
    {
        private LiteContainer _container;
        private BuildKey _key;
        private IList<IBuilderPolicy> _policies;
=======
namespace Wayne.Payment.Platform
{
    public class ContainerRegistration
    {
        private LiteContainer _container;
        private BuildKey _key;
        private readonly IList<IBuilderPolicy> _policies;
>>>>>>> 06f38426eb2a120e3f5be0a79f2c3cf88f9ff4e4
        private Dictionary<Type, ContainerRegistration> _registrations;

        internal ContainerRegistration(LiteContainer container, BuildKey key, IList<IBuilderPolicy> policies)
        {
            _container = container;
            _key = key;
            _policies = policies;
            _registrations = new Dictionary<Type, ContainerRegistration>();
        }

        public IDictionary<Type, ContainerRegistration> Registrations
        {
            get { return _registrations; }
        }

<<<<<<< HEAD
        public Type ServiceType
        {
            get { return _key.Type; }
        }

        public string Name
        {
            get { return _key.Name; }
        }

=======
>>>>>>> 06f38426eb2a120e3f5be0a79f2c3cf88f9ff4e4
        public ContainerRegistration As<TService>()
        {
            var serviceType = typeof (TService);

            if (!serviceType.IsInterface)
                throw new InvalidOperationException(string.Format("{0} must be an interface type", serviceType.Name));

            return Register(serviceType);
        }

        public ContainerRegistration As(Type serviceType)
        {
            if (serviceType == null)
                throw new ArgumentNullException("serviceType");

            if (!serviceType.IsInterface)
                throw new InvalidOperationException(string.Format("{0} must be an interface type", serviceType.Name));

            return Register(serviceType);
        }

        public ContainerRegistration As(Type[] serviceTypes)
        {
            if (serviceTypes == null)
                throw new ArgumentNullException("serviceTypes");

            foreach (var serviceType in serviceTypes)
            {
                if (serviceType == null)
                    throw new ArgumentException("serviceTypes cannot contain a {null} entry");

                if (!serviceType.IsInterface)
                    throw new InvalidOperationException(string.Format("{0} must be an interface type", serviceType.Name));

                if (!serviceType.IsAssignableFrom(_key.Type))
                    throw new ArgumentException(string.Format("{0} is not assignable from {1}", serviceType.Name, _key.Type.Name));

                Register(serviceType);
            }

            return this;
        }

        public ContainerRegistration Singleton()
        {
            _policies.Set((ILifetimePolicy) new SingletonLifetimePolicy());
            return this;
        }

        public ContainerRegistration Transient()
        {
            _policies.Set((ILifetimePolicy) new TransientLifetimePolicy());
            return this;
        }

        public ContainerRegistration External()
        {
            _policies.Set((ILifetimePolicy) new ExternalLifetimePolicy());
            return this;
        }

        public ContainerRegistration PerThread()
        {
            _policies.Set((ILifetimePolicy)new PerThreadLifetimePolicy());
            return this;
        }

        public ContainerRegistration Parameters(params Type[] paramTypes)
        {
            if (paramTypes.Any())
                _policies.Set<IConstructorPolicy>(new InjectionConstructorPolicy(paramTypes));
            return this;
        }

        public ContainerRegistration Property(Type propType)
        {
            if (propType == null)
                throw new ArgumentNullException("propType");

            if (!propType.IsInterface)
                throw new InvalidOperationException("Property must be an interface type");

            _policies.AddPolicies(new InjectionPropertyTypePolicy(propType));

            return this;
        }

        public ContainerRegistration Property(string propName)
        {
            if (string.IsNullOrEmpty(propName))
                throw new ArgumentNullException("propName");

            var property = _key.Type.GetProperties(BindingFlags.Public | BindingFlags.Instance).FirstOrDefault();

            if (property == null)
            {
                throw new ArgumentException(string.Format("{0} does not have a public non static property named {1}",
                    _key.Type.Name, propName));
            }

            if (!property.PropertyType.IsInterface)
                throw new InvalidOperationException("Property must be an interface type");

            _policies.AddPolicies(new InjectionPropertyNamePolicy(propName));

            return this;
        }

        private ContainerRegistration Register(Type serviceType)
        {
            BuildKey newKey = BuildKey.Create(serviceType, _key.Name);

            ContainerRegistration registration;

            if (_key.Type.IsSubclassOf(typeof(Delegate)))
            {
                if (_key.Type.GetMethod("Invoke").ReturnType != serviceType)
                    throw new ArgumentException(string.Format("Factory function does not return {0} type", serviceType.Name));

                registration = _container.Register(newKey, new BuildType(_key.Type), new NamedPolicy(_key.Name), new ExternalLifetimePolicy(),
                    new FactoryFunctionPolicy(_key), new FactoryBuildPlanPolicy());
            }
            else
            {
                if (!serviceType.IsAssignableFrom(_key.Type))
                    throw new ArgumentException(string.Format("{0} is not assignable from {1}", serviceType.Name, _key.Type.Name));

                registration = _container.Register(newKey, new BuildType(_key.Type), new NamedPolicy(_key.Name), new TransientLifetimePolicy(),
                    new ResolveBuildPlanPolicy());
            }

            _registrations.Add(serviceType, registration);

            return this;
        }
    }
}
