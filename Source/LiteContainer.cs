using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.ServiceLocation;

namespace Wayne.Payment.Platform
{
    public class LiteContainer : ILiteContainer
    {
        private bool _disposed;
        private object _syncRoot = new object();
        private Dictionary<BuildKey, List<IBuilderPolicy>> _registeredServices;
        private LifetimeContainer _lifetime;
        private List<ContainerRegistration> _registrations;

        public LiteContainer()
        {
            _registeredServices = new Dictionary<BuildKey, List<IBuilderPolicy>>();
            _registrations = new List<ContainerRegistration>();
            _lifetime = new LifetimeContainer();
        }

        ~LiteContainer()
        {
            Dispose(false);
        }

        public IEnumerable<ContainerRegistration> Registrations
        {
            get { return _registrations; }
        }

        public ContainerRegistration Register(object service)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            return Register(service.GetType(), string.Empty, new SingletonLifetimePolicy(service));
        }

        public ContainerRegistration Register(object service, string name)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            return Register(service.GetType(), name, new SingletonLifetimePolicy(service));
        }

        public ContainerRegistration Register(Delegate factory)
        {
            if (factory == null)
                throw new ArgumentNullException("factory");

            return Register(factory.GetType(), string.Empty, new SingletonLifetimePolicy(factory));
        }

        public ContainerRegistration Register(Delegate factory, string name)
        {
            if (factory == null)
                throw new ArgumentNullException("factory");

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            return Register(factory.GetType(), name, new SingletonLifetimePolicy(factory));
        }

        public ContainerRegistration Register<TService>(Func<ResolveParameters, TService> factory)
        {
            if (factory == null)
                throw new ArgumentNullException("factory");

            return Register(factory.GetType(), string.Empty, new SingletonLifetimePolicy(factory));
        }

        public ContainerRegistration Register<TService>(Func<ResolveParameters, TService> factory, string name)
        {
            if (factory == null)
                throw new ArgumentNullException("factory");

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            return Register(factory.GetType(), name, new SingletonLifetimePolicy(factory));
        }

        public ContainerRegistration Register(Type implementationType)
        {
            if (implementationType == null)
                throw new ArgumentNullException("implementationType");

            return Register(implementationType, string.Empty, new DefaultConstructorPolicy(), new TransientLifetimePolicy(),
               new DefaultBuildPlanPolicy());
        }

        public ContainerRegistration Register(Type implementationType, string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            return Register(implementationType, name, new DefaultConstructorPolicy(), new TransientLifetimePolicy(),
               new DefaultBuildPlanPolicy());
        }

        public ContainerRegistration Register<TImplementation>() 
            where TImplementation : class
        {
            return Register(typeof(TImplementation), string.Empty, new DefaultConstructorPolicy(), new TransientLifetimePolicy(),
                new DefaultBuildPlanPolicy());
        }

        public ContainerRegistration Register<TImplementation>(string name) 
            where TImplementation : class
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            return Register(typeof(TImplementation), name, new DefaultConstructorPolicy(), new TransientLifetimePolicy(),
                new DefaultBuildPlanPolicy());
        }

        private ContainerRegistration Register(Type implementationType, string name, params IBuilderPolicy[] policies)
        {
            var key = BuildKey.Create(implementationType, name);

            var newPolicies = new List<IBuilderPolicy>
                {
                    new BuildType(implementationType),
                    new NamedPolicy(name)
                };

            newPolicies.AddRange(policies);

            return Register(key, newPolicies.ToArray());
        }

        internal ContainerRegistration Register(BuildKey key, params IBuilderPolicy[] policies)
        {
            ThrowIfDisposed();

            if (!_registeredServices.ContainsKey(key))
                _registeredServices.Add(key, new List<IBuilderPolicy>(policies));
            else
                _registeredServices[key] = new List<IBuilderPolicy>(policies);

            var registration = new ContainerRegistration(this, key, _registeredServices[key]);

            _registrations.Add(registration);

            return registration;
        }

        public object Resolve(Type serviceType)
        {
            var key = new BuildKey(serviceType);
            return Resolve(key);
        }

        public object Resolve(Type serviceType, params ResolveParameter[] resolverOverrides)
        {
            var key = new BuildKey(serviceType);
            return Resolve(key, resolverOverrides);
        }

        public TService Resolve<TService>()
        {
            var key = new BuildKey(typeof(TService));
            return (TService)Resolve(key);
        }

        public TService Resolve<TService>(params ResolveParameter[] resolverOverrides)
        {
            var key = new BuildKey(typeof(TService));
            return (TService)Resolve(key, resolverOverrides);
        }

        public object Resolve(Type serviceType, string name)
        {
            var key = new BuildKey(serviceType, name);
            return Resolve(key);
        }

        public object Resolve(Type serviceType, string name, params ResolveParameter[] resolverOverrides)
        {
            var key = new BuildKey(serviceType, name);
            return Resolve(key, resolverOverrides);
        }

        public TService Resolve<TService>(string name)
        {
            var key = new BuildKey(typeof (TService), name);
            return (TService) Resolve(key);
        }

        public TService Resolve<TService>(string name, params ResolveParameter[] resolverOverrides)
        {
            var key = new BuildKey(typeof(TService), name);
            return (TService)Resolve(key, resolverOverrides);
        }

        public IEnumerable<object> ResolveAll(Type serviceType)
        {
            var key = new BuildKey(serviceType);
            return _registeredServices.Where(p => Equals(p.Key, key)).Select(p => Resolve(p.Key));
        }

        public IEnumerable<TService> ResolveAll<TService>()
        {
            var key = new BuildKey(typeof (TService));
            return _registeredServices.Where(p => Equals(p.Key, key)).Select(p => Resolve(p.Key)).Cast<TService>();
        }

        private object Resolve(BuildKey key, params ResolveParameter[] resolverOverrides)
        {
            ThrowIfDisposed();

            object service;

            try
            {
                if (key == null)
                    throw new InvalidOperationException(string.Format("Invalid key"));

                if (!_registeredServices.ContainsKey(key))
                    throw new InvalidOperationException(string.Format("Not registered:{0}", key));

                var policies = _registeredServices[key];

                if (resolverOverrides != null)
                {
                    foreach (var resolverOverride in resolverOverrides)
                    {
                        resolverOverride.AddPolicies(policies);
                    }
                }

                service = NewBuildUp(key, policies);
            }
            catch (Exception ex)
            {
                throw new ActivationException(string.Format("Cannot resolve ({0})", key), ex);
            }

            return service;
        }

        public object BuildUp(object service)
        {
            return BuildUp(BuildKey.Create(service.GetType()), service);
        }

        public object BuildUp(object service, string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            return BuildUp(BuildKey.Create(service.GetType(), name), service);
        }

        private object BuildUp(BuildKey key, object service)
        {
            if (!_registeredServices.ContainsKey(key))
                return service;

            var policies = _registeredServices[key];

            var context = new BuilderContext(this, key, policies, _lifetime)
                {
                    Current = service
                };

            var injectionPolicies = policies.OfType<IInjectionPolicy>();

            foreach (var injectionPolicy in injectionPolicies)
            {
                injectionPolicy.Inject(context);
            }

            return service;
        }

        public void TearDown(object service)
        {
            var disposable = service as IDisposable;
            if (disposable != null)
                disposable.Dispose();
        }

        internal object NewBuildUp(BuildKey key, params IResolveParameterPolicy[] resolverPolicies)
        {
            if (!_registeredServices.ContainsKey(key))
                return null;

            var policies = _registeredServices[key];
            policies.AddRange(resolverPolicies);

            return NewBuildUp(key, policies);
        }

        internal object NewBuildUp(BuildKey key, IList<IBuilderPolicy> policies)
        {
            var lifetimePolicy = policies.OfType<ILifetimePolicy>().Single();

            object service = lifetimePolicy.GetValue();

            if (service != null)
                return service;

            var context = new BuilderContext(this, key, policies, _lifetime);

            var buildPlanPolicy = policies.OfType<IBuildPlanPolicy>().Single();

            service = buildPlanPolicy.BuildUp(context);

            if (service != null)
                lifetimePolicy.SetValue(service);
            else
                throw new InvalidOperationException("Cannot create instance");

            if (lifetimePolicy is IDisposable)
                _lifetime.Add(lifetimePolicy);

            return service;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            lock(_syncRoot)
            {
                if (_disposed)
                    return;

                if (disposing)
                {
                    _lifetime.Dispose();
                }

                //cleanup unmanaged resources 

                _disposed = true;
            }
        }

        private void ThrowIfDisposed()
        {
            lock(_syncRoot) if (_disposed) throw new ObjectDisposedException("LiteContainer");
        }
    }
}
