using System;
using System.Collections.Generic;

<<<<<<< HEAD
namespace Wayne.Payment.Platform.Lite
=======
namespace Wayne.Payment.Platform
>>>>>>> 06f38426eb2a120e3f5be0a79f2c3cf88f9ff4e4
{
    public interface ILiteContainer : IDisposable
    {
        ContainerRegistration Register(object service);

        ContainerRegistration Register(object service, string name);

<<<<<<< HEAD
        ContainerRegistration Register<TService>(Func<TService> factory);

        ContainerRegistration Register<TService>(Func<TService> factory, string name);

        ContainerRegistration Register<TService>(Func<object[], TService> factory);

        ContainerRegistration Register<TService>(Func<object[], TService> factory, string name);
=======
        ContainerRegistration Register(Delegate factory);

        ContainerRegistration Register(Delegate factory, string name);
>>>>>>> 06f38426eb2a120e3f5be0a79f2c3cf88f9ff4e4

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
