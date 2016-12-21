using System;
using System.Collections.Generic;

namespace Wayne.Payment.Platform
{
    public interface ILiteContainer : IDisposable
    {
        ContainerRegistration Register(object service);

        ContainerRegistration Register(object service, string name);

        ContainerRegistration Register(Delegate factory);

        ContainerRegistration Register(Delegate factory, string name);

        ContainerRegistration Register<TService>(Func<ResolveParameters, TService> factory);

        ContainerRegistration Register<TService>(Func<ResolveParameters, TService> factory, string name);

        ContainerRegistration Register(Type implementationType);

        ContainerRegistration Register(Type implementationType, string name);

        ContainerRegistration Register<TImplementation>()
            where TImplementation : class;

        ContainerRegistration Register<TImplementation>(string name)
            where TImplementation : class;

        object Resolve(Type serviceType);

        object Resolve(Type serviceType, params ResolveParameter[] resolverOverrides);

        TService Resolve<TService>();

        TService Resolve<TService>(params ResolveParameter[] resolverOverrides);

        object Resolve(Type serviceType, string name);

        object Resolve(Type serviceType, string name, params ResolveParameter[] resolverOverrides);

        TService Resolve<TService>(string name);

        TService Resolve<TService>(string name, params ResolveParameter[] resolverOverrides);

        IEnumerable<object> ResolveAll(Type serviceType);

        IEnumerable<TService> ResolveAll<TService>();

        object BuildUp(object service);

        object BuildUp(object service, string name);

        void TearDown(object service);

        IEnumerable<ContainerRegistration> Registrations { get; }
    }
}
