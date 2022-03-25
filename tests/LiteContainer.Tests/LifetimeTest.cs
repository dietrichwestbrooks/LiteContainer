

// ReSharper disable ConvertToConstant.Local
namespace LiteContainer
{
    using System;
    using System.Threading;
    using Xunit;
    using Xunit.Sdk;

    /// <summary>
    /// Summary description for LifetimeTest
    /// </summary>
    public class LifetimeTest : IDisposable
    {
        private readonly ILiteContainer _container;

        public LifetimeTest()
        {
            _container = new LiteContainer();
        }

        // Use Dispose to run code after each test has run
        public void Dispose()
        {
            _container.Dispose();
        }

        [Fact]
        public void verify_per_resolve_lifetime_for_registered_type()
        {
            var expectedType = typeof(TestService);

            _container.Register(expectedType)
                .PerThread();

            var actualInstance = _container.Resolve(expectedType);

            Assert.NotNull(actualInstance);
            Assert.IsType<TestService>(actualInstance);

            var actualInstance2 = _container.Resolve(expectedType);

            Assert.NotNull(actualInstance2);
            Assert.IsType<TestService>(actualInstance2);

            Assert.NotSame(actualInstance, actualInstance2);
        }

        [Fact]
        public void verify_per_resolve_lifetime_is_default_for_registered_type()
        {
            var expectedType = typeof(TestService);

            _container.Register(expectedType);

            var actualInstance = _container.Resolve(expectedType);

            Assert.NotNull(actualInstance);
            Assert.IsType<TestService>(actualInstance);

            var actualInstance2 = _container.Resolve(expectedType);

            Assert.NotNull(actualInstance2);
            Assert.IsType<TestService>(actualInstance2);

            Assert.NotSame(actualInstance, actualInstance2);
        }

        [Fact]
        public void verify_per_resolve_lifetime_for_factory_registered_type()
        {
            var expectedType = typeof(ITestService);
            var factory = new Func<ITestService>(() => new TestService());

            var registration = _container.Register(factory);
            registration.Registrations[expectedType].PerThread();

            var actualInstance = _container.Resolve(expectedType);

            Assert.NotNull(actualInstance);
            Assert.IsType<ITestService>(actualInstance);

            var actualInstance2 = _container.Resolve(expectedType);

            Assert.NotNull(actualInstance2);
            Assert.IsType<ITestService>(actualInstance2);

            Assert.NotSame(actualInstance, actualInstance2);
        }

        [Fact]
        public void verify_singleton_lifetime_for_registered_type()
        {
            var expectedType = typeof(TestService);

            _container.Register(expectedType)
                .Singleton();

            var actualInstance = _container.Resolve(expectedType);

            Assert.NotNull(actualInstance);
            Assert.IsType<TestService>(actualInstance);

            var actualInstance2 = _container.Resolve(expectedType);

            Assert.NotNull(actualInstance2);
            Assert.IsType<TestService>(actualInstance2);

            Assert.Same(actualInstance, actualInstance2);
        }

        [Fact]
        public void verify_singleton_lifetime_for_factory_registered_type()
        {
            var expectedType = typeof(ITestService);
            var factory = new Func<ITestService>(() => new TestService());

            var registration = _container.Register(factory);
            registration.Registrations[expectedType].Singleton();

            var actualInstance = _container.Resolve(expectedType);

            Assert.NotNull(actualInstance);
            Assert.IsType<ITestService>(actualInstance);

            var actualInstance2 = _container.Resolve(expectedType);

            Assert.NotNull(actualInstance2);
            Assert.IsType<ITestService>(actualInstance2);

            Assert.Same(actualInstance, actualInstance2);
        }

        [Fact]
        public void verify_singleton_lifetime_for_registered_instance()
        {
            var expectedType = typeof(TestService);

            var registeredInstance = new TestService();

            _container.Register(registeredInstance)
                .Singleton();

            var actualInstance = _container.Resolve(expectedType);

            Assert.NotNull(actualInstance);
            Assert.IsType<TestService>(actualInstance);

            var actualInstance2 = _container.Resolve(expectedType);

            Assert.NotNull(actualInstance2);
            Assert.IsType<TestService>(actualInstance2);

            Assert.Same(actualInstance, actualInstance2);
        }

        [Fact]
        public void verify_singleton_lifetime_is_default_for_registered_instance()
        {
            var expectedType = typeof(TestService);

            var registeredInstance = new TestService();

            _container.Register(registeredInstance);

            var actualInstance = _container.Resolve(expectedType);

            Assert.NotNull(actualInstance);
            Assert.IsType<TestService>(actualInstance);

            var actualInstance2 = _container.Resolve(expectedType);

            Assert.NotNull(actualInstance2);
            Assert.IsType<TestService>(actualInstance2);

            Assert.Same(actualInstance, actualInstance2);
        }

        [Fact]
        public void verify_external_lifetime_for_registered_instance()
        {
            var expectedType = typeof(TestService);

            var registeredInstance = new TestService();

            _container.Register(registeredInstance)
                .External();

            var actualInstance = _container.Resolve(expectedType);

            Assert.NotNull(actualInstance);
            Assert.IsType<ITestService>(actualInstance);

            var actualInstance2 = _container.Resolve(expectedType);

            Assert.NotNull(actualInstance2);
            Assert.IsType<ITestService>(actualInstance2);

            Assert.Same(actualInstance, actualInstance2);
        }

        [Fact]
        public void verify_external_lifetime_for_registered_type()
        {
            var expectedType = typeof(TestService);

            _container.Register(expectedType)
                .External();

            var actualInstance = _container.Resolve(expectedType);

            Assert.NotNull(actualInstance);
            Assert.IsType<ITestService>(actualInstance);

            var actualInstance2 = _container.Resolve(expectedType);

            Assert.NotNull(actualInstance2);
            Assert.IsType<ITestService>(actualInstance2);

            Assert.Same(actualInstance, actualInstance2);
        }

        [Fact]
        public void verify_external_lifetime_for_factory_registered_type()
        {
            var expectedType = typeof(ITestService);
            var factory = new Func<ITestService>(() => new TestService());

            var registration = _container.Register(factory);
            registration.Registrations[expectedType].External();

            var actualInstance = _container.Resolve(expectedType);

            Assert.NotNull(actualInstance);
            Assert.IsType<ITestService>(actualInstance);

            var actualInstance2 = _container.Resolve(expectedType);

            Assert.NotNull(actualInstance2);
            Assert.IsType<ITestService>(actualInstance2);

            Assert.Same(actualInstance, actualInstance2);
        }

        [Fact]
        public void verify_external_lifetime_for_registered_type_when_disposed()
        {
            var expectedType = typeof(ITestService);

            var expectedValue1 = 100;
            var expectedValue2 = 200;

            _container.Register<TestService>()
                .Parameters(typeof (int))
                .External();

            var runIsolated = new Func<WeakReference>(() =>
            {
                var actualInstance =
                    _container.Resolve<TestService>(new NamedParameter<int>("value", expectedValue1)) as ITestService;

                Assert.NotNull(actualInstance);
                Assert.IsType<ITestService>(actualInstance);

// ReSharper disable PossibleNullReferenceException
                var actualValue1 = actualInstance.Value;
// ReSharper restore PossibleNullReferenceException

                Assert.Equal(expectedValue1, actualValue1);

                return new WeakReference(actualInstance);
            });

            var reference = runIsolated();

            GC.Collect(0, GCCollectionMode.Forced);
            GC.WaitForFullGCComplete();

            Assert.False(reference.IsAlive, "Instance reference should have been garbage collected");

            // NamedParameter should be used because instance needs to be created
            var actualInstance2 = _container.Resolve<TestService>(
                new NamedParameter<int>("value", expectedValue2)) as ITestService;

            Assert.NotNull(actualInstance2);
            Assert.IsType<ITestService>(actualInstance2);

// ReSharper disable PossibleNullReferenceException
            var actualValue2 = actualInstance2.Value;
// ReSharper restore PossibleNullReferenceException

            Assert.Equal(expectedValue2, actualValue2);
        }

        [Fact]
        public void verify_external_lifetime_for_factory_registered_type_when_disposed()
        {
            var expectedType = typeof(ITestService);

            var expectedValue1 = 100;
            var expectedValue2 = 200;

            var factory = new Func<ResolveParameters, ITestService>(p => new TestService(p.NamedParameter<int>("value")));

            var registration = _container.Register(factory);
            registration.Registrations[expectedType].External();

            var runIsolated = new Func<WeakReference>(() =>
            {
                var actualInstance = _container.Resolve(expectedType, 
                    new NamedParameter<int>("value", expectedValue1)) as ITestService;

                Assert.NotNull(actualInstance);
                Assert.IsType<ITestService>(actualInstance);

// ReSharper disable PossibleNullReferenceException
                var actualValue1 = actualInstance.Value;
// ReSharper restore PossibleNullReferenceException

                Assert.Equal(expectedValue1, actualValue1);

                return new WeakReference(actualInstance);
            });

            var reference = runIsolated();

            GC.Collect(0, GCCollectionMode.Forced);
            GC.WaitForFullGCComplete();

            Assert.False(reference.IsAlive, "Instance reference should have been garbage collected");

            // NamedParameter should be used because instance needs to be created
            var actualInstance2 = _container.Resolve(expectedType,
                new NamedParameter<int>("value", expectedValue2)) as ITestService;

            Assert.NotNull(actualInstance2);
            Assert.IsType<ITestService>(actualInstance2);

// ReSharper disable PossibleNullReferenceException
            var actualValue2 = actualInstance2.Value;
// ReSharper restore PossibleNullReferenceException

            Assert.Equal(expectedValue2, actualValue2);
        }

        [Fact]
        public void verify_per_thread_lifetime_for_registered_type_in_same_thread()
        {
            var expectedType = typeof(ITestService);

            var expectedValue1 = 100;
            var expectedValue2 = expectedValue1;

            _container.Register<TestService>()
                .Parameters(typeof(int))
                .PerThread();

            var actualInstance =
                _container.Resolve<TestService>(new NamedParameter<int>("value", expectedValue1)) as ITestService;

            Assert.NotNull(actualInstance);
            Assert.IsType<ITestService>(actualInstance);

            // ReSharper disable PossibleNullReferenceException
            var actualValue1 = actualInstance.Value;
            // ReSharper restore PossibleNullReferenceException

            Assert.Equal(expectedValue1, actualValue1);

            // NamedParameter will be ignored cause no instance needs to be created in same thread
            var actualInstance2 = _container.Resolve<TestService>(
                        new NamedParameter<int>("value", 300)) as ITestService;

            Assert.NotNull(actualInstance2);
            Assert.IsType<ITestService>(actualInstance2);

            Assert.Same(actualInstance, actualInstance2);

            var actualValue2 = actualInstance2.Value;

            Assert.Equal(expectedValue2, actualValue2);
        }

        [Fact]
        public void verify_per_thread_lifetime_for_factory_registered_type_in_same_thread()
        {
            var expectedType = typeof(ITestService);

            var expectedValue1 = 100;
            var expectedValue2 = expectedValue1;

            var factory = new Func<ResolveParameters, ITestService>(p => new TestService(p.NamedParameter<int>("value")));

            var registration = _container.Register(factory);
            registration.Registrations[expectedType].PerThread();

            var actualInstance =
                _container.Resolve(expectedType, new NamedParameter<int>("value", expectedValue1)) as ITestService;

            Assert.NotNull(actualInstance);
            Assert.IsType<ITestService>(actualInstance);

// ReSharper disable PossibleNullReferenceException
            var actualValue1 = actualInstance.Value;
// ReSharper restore PossibleNullReferenceException

            Assert.Equal(expectedValue1, actualValue1);

            // NamedParameter will be ignored cause no instance needs to be created in same thread
            var actualInstance2 = _container.Resolve(expectedType,
                        new NamedParameter<int>("value", 300)) as ITestService;

            Assert.NotNull(actualInstance2);
            Assert.IsType<ITestService>(actualInstance2);

            Assert.Same(actualInstance, actualInstance2);

// ReSharper disable PossibleNullReferenceException
            var actualValue2 = actualInstance2.Value;
// ReSharper restore PossibleNullReferenceException

            Assert.Equal(expectedValue2, actualValue2);
        }

        [Fact]
        public void verify_per_thread_lifetime_for_registered_type_in_different_thread()
        {
            var expectedType = typeof (ITestService);

            var expectedValue1 = 100;
            var expectedValue2 = 200;

            _container.Register<TestService>()
                .Parameters(typeof (int))
                .PerThread();

            var actualInstance =
                _container.Resolve<TestService>(new NamedParameter<int>("value", expectedValue1)) as ITestService;

            Assert.NotNull(actualInstance);
            Assert.IsType<ITestService>(actualInstance);

// ReSharper disable PossibleNullReferenceException
            var actualValue1 = actualInstance.Value;
// ReSharper restore PossibleNullReferenceException

            Assert.Equal(expectedValue1, actualValue1);

            ITestService actualInstance2 = null;

            var t = new Thread(state =>
            {
                try
                {
                    // NamedParameter should be used because instance needs to be created in new thread
                    actualInstance2 = _container.Resolve<TestService>(
                        new NamedParameter<int>("value", expectedValue2));
                }
                catch (Exception)
                {
                }
            });

            t.Start();

            if (!t.Join(1000))
                throw new XunitException("Timeout wating for thread");

            Assert.NotNull(actualInstance2);
            Assert.IsType<ITestService>(actualInstance2);

            Assert.NotSame(actualInstance, actualInstance2);

            var actualValue2 = actualInstance2.Value;

            Assert.Equal(expectedValue2, actualValue2);
        }

        [Fact]
        public void verify_per_thread_lifetime_for_factory_registered_type_in_different_thread()
        {
            var expectedType = typeof(ITestService);

            var expectedValue1 = 100;
            var expectedValue2 = 200;

            var factory = new Func<ResolveParameters, ITestService>(p => new TestService(p.NamedParameter<int>("value")));

            var registration = _container.Register(factory);
            registration.Registrations[expectedType].PerThread();

            var actualInstance =
                _container.Resolve(expectedType, new NamedParameter<int>("value", expectedValue1)) as ITestService;

            Assert.NotNull(actualInstance);
            Assert.IsType<ITestService>(actualInstance);

// ReSharper disable PossibleNullReferenceException
            var actualValue1 = actualInstance.Value;
// ReSharper restore PossibleNullReferenceException

            Assert.Equal(expectedValue1, actualValue1);

            ITestService actualInstance2 = null;

            var t = new Thread(state =>
            {
                try
                {
                    // NamedParameter should be used because instance needs to be created in new thread
                    actualInstance2 = _container.Resolve(expectedType,
                        new NamedParameter<int>("value", expectedValue2)) as ITestService;
                }
                catch (Exception)
                {
                }
            });

            t.Start();

            if (!t.Join(1000))
                throw new XunitException("Timeout wating for thread");

            Assert.NotNull(actualInstance2);
            Assert.IsType<ITestService>(actualInstance2);

            Assert.NotSame(actualInstance, actualInstance2);

            var actualValue2 = actualInstance2.Value;

            Assert.Equal(expectedValue2, actualValue2);
        }

        [Fact]
        public void verify_container_dispose()
        {
            TestService actualInstance = null;

            var runIsolated = new Func<WeakReference>(() =>
            {
                using (var container = new LiteContainer())
                {
                    container.Register<TestService>()
                        .Singleton();

                    actualInstance = container.Resolve<TestService>();

                    return new WeakReference(container, false);
                }
            });

            var reference = runIsolated();

            GC.Collect(0, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();

            Assert.False(reference.IsAlive, "Container should have been garbage collected");

            Assert.NotNull(actualInstance);

            Assert.True(actualInstance.IsDisposed);
        }
    }
}
