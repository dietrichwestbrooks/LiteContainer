using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wayne.Payment.Platform.Lite;

// ReSharper disable ConvertToConstant.Local
namespace LiteContainer.Test
{
    /// <summary>
    /// Summary description for LifetimeTest
    /// </summary>
    [TestClass]
    public class LifetimeTest
    {
        private ILiteContainer _container;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        [TestInitialize]
        public void TestInitialize()
        {
            _container = new Wayne.Payment.Platform.Lite.LiteContainer();
        }

        // Use TestCleanup to run code after each test has run
        [TestCleanup]
        public void TestCleanup()
        {
            _container.Dispose();
        }
        #endregion

        [TestMethod]
        public void verify_per_resolve_lifetime_for_registered_type()
        {
            var expectedType = typeof(TestService);

            _container.Register(expectedType)
                .PerResolve();

            var actualInstance = _container.Resolve(expectedType);

            Assert.IsNotNull(actualInstance);
            Assert.IsInstanceOfType(actualInstance, expectedType);

            var actualInstance2 = _container.Resolve(expectedType);

            Assert.IsNotNull(actualInstance2);
            Assert.IsInstanceOfType(actualInstance2, expectedType);

            Assert.AreNotSame(actualInstance, actualInstance2);
        }

        [TestMethod]
        public void verify_per_resolve_lifetime_is_default_for_registered_type()
        {
            var expectedType = typeof(TestService);

            _container.Register(expectedType);

            var actualInstance = _container.Resolve(expectedType);

            Assert.IsNotNull(actualInstance);
            Assert.IsInstanceOfType(actualInstance, expectedType);

            var actualInstance2 = _container.Resolve(expectedType);

            Assert.IsNotNull(actualInstance2);
            Assert.IsInstanceOfType(actualInstance2, expectedType);

            Assert.AreNotSame(actualInstance, actualInstance2);
        }

        [TestMethod]
        public void verify_per_resolve_lifetime_for_factory_registered_type()
        {
            var expectedType = typeof(ITestService);
            var factory = new Func<ITestService>(() => new TestService());

            var registration = _container.Register(factory);
            registration.Registrations[expectedType].PerResolve();

            var actualInstance = _container.Resolve(expectedType);

            Assert.IsNotNull(actualInstance);
            Assert.IsInstanceOfType(actualInstance, expectedType);

            var actualInstance2 = _container.Resolve(expectedType);

            Assert.IsNotNull(actualInstance2);
            Assert.IsInstanceOfType(actualInstance2, expectedType);

            Assert.AreNotSame(actualInstance, actualInstance2);
        }

        [TestMethod]
        public void verify_singleton_lifetime_for_registered_type()
        {
            var expectedType = typeof(TestService);

            _container.Register(expectedType)
                .Singleton();

            var actualInstance = _container.Resolve(expectedType);

            Assert.IsNotNull(actualInstance);
            Assert.IsInstanceOfType(actualInstance, expectedType);

            var actualInstance2 = _container.Resolve(expectedType);

            Assert.IsNotNull(actualInstance2);
            Assert.IsInstanceOfType(actualInstance2, expectedType);

            Assert.AreSame(actualInstance, actualInstance2);
        }

        [TestMethod]
        public void verify_singleton_lifetime_for_factory_registered_type()
        {
            var expectedType = typeof(ITestService);
            var factory = new Func<ITestService>(() => new TestService());

            var registration = _container.Register(factory);
            registration.Registrations[expectedType].Singleton();

            var actualInstance = _container.Resolve(expectedType);

            Assert.IsNotNull(actualInstance);
            Assert.IsInstanceOfType(actualInstance, expectedType);

            var actualInstance2 = _container.Resolve(expectedType);

            Assert.IsNotNull(actualInstance2);
            Assert.IsInstanceOfType(actualInstance2, expectedType);

            Assert.AreSame(actualInstance, actualInstance2);
        }

        [TestMethod]
        public void verify_singleton_lifetime_for_registered_instance()
        {
            var expectedType = typeof(TestService);

            var registeredInstance = new TestService();

            _container.Register(registeredInstance)
                .Singleton();

            var actualInstance = _container.Resolve(expectedType);

            Assert.IsNotNull(actualInstance);
            Assert.IsInstanceOfType(actualInstance, expectedType);

            var actualInstance2 = _container.Resolve(expectedType);

            Assert.IsNotNull(actualInstance2);
            Assert.IsInstanceOfType(actualInstance2, expectedType);

            Assert.AreSame(actualInstance, actualInstance2);
        }

        [TestMethod]
        public void verify_singleton_lifetime_is_default_for_registered_instance()
        {
            var expectedType = typeof(TestService);

            var registeredInstance = new TestService();

            _container.Register(registeredInstance);

            var actualInstance = _container.Resolve(expectedType);

            Assert.IsNotNull(actualInstance);
            Assert.IsInstanceOfType(actualInstance, expectedType);

            var actualInstance2 = _container.Resolve(expectedType);

            Assert.IsNotNull(actualInstance2);
            Assert.IsInstanceOfType(actualInstance2, expectedType);

            Assert.AreSame(actualInstance, actualInstance2);
        }

        [TestMethod]
        public void verify_external_lifetime_for_registered_instance()
        {
            var expectedType = typeof(TestService);

            var registeredInstance = new TestService();

            _container.Register(registeredInstance)
                .External();

            var actualInstance = _container.Resolve(expectedType);

            Assert.IsNotNull(actualInstance);
            Assert.IsInstanceOfType(actualInstance, expectedType);

            var actualInstance2 = _container.Resolve(expectedType);

            Assert.IsNotNull(actualInstance2);
            Assert.IsInstanceOfType(actualInstance2, expectedType);

            Assert.AreSame(actualInstance, actualInstance2);
        }

        [TestMethod]
        public void verify_external_lifetime_for_registered_type()
        {
            var expectedType = typeof(TestService);

            _container.Register(expectedType)
                .External();

            var actualInstance = _container.Resolve(expectedType);

            Assert.IsNotNull(actualInstance);
            Assert.IsInstanceOfType(actualInstance, expectedType);

            var actualInstance2 = _container.Resolve(expectedType);

            Assert.IsNotNull(actualInstance2);
            Assert.IsInstanceOfType(actualInstance2, expectedType);

            Assert.AreSame(actualInstance, actualInstance2);
        }

        [TestMethod]
        public void verify_external_lifetime_for_factory_registered_type()
        {
            var expectedType = typeof(ITestService);
            var factory = new Func<ITestService>(() => new TestService());

            var registration = _container.Register(factory);
            registration.Registrations[expectedType].External();

            var actualInstance = _container.Resolve(expectedType);

            Assert.IsNotNull(actualInstance);
            Assert.IsInstanceOfType(actualInstance, expectedType);

            var actualInstance2 = _container.Resolve(expectedType);

            Assert.IsNotNull(actualInstance2);
            Assert.IsInstanceOfType(actualInstance2, expectedType);

            Assert.AreSame(actualInstance, actualInstance2);
        }

        [TestMethod]
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

                Assert.IsNotNull(actualInstance);
                Assert.IsInstanceOfType(actualInstance, expectedType);

// ReSharper disable PossibleNullReferenceException
                var actualValue1 = actualInstance.Value;
// ReSharper restore PossibleNullReferenceException

                Assert.AreEqual(expectedValue1, actualValue1);

                return new WeakReference(actualInstance);
            });

            var reference = runIsolated();

            GC.Collect(0, GCCollectionMode.Forced);
            GC.WaitForFullGCComplete();

            Assert.IsFalse(reference.IsAlive, "Instance reference should have been garbage collected");

            // NamedParameter should be used because instance needs to be created
            var actualInstance2 = _container.Resolve<TestService>(
                new NamedParameter<int>("value", expectedValue2)) as ITestService;

            Assert.IsNotNull(actualInstance2);
            Assert.IsInstanceOfType(actualInstance2, expectedType);

// ReSharper disable PossibleNullReferenceException
            var actualValue2 = actualInstance2.Value;
// ReSharper restore PossibleNullReferenceException

            Assert.AreEqual(expectedValue2, actualValue2);
        }

        [TestMethod]
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

                Assert.IsNotNull(actualInstance);
                Assert.IsInstanceOfType(actualInstance, expectedType);

// ReSharper disable PossibleNullReferenceException
                var actualValue1 = actualInstance.Value;
// ReSharper restore PossibleNullReferenceException

                Assert.AreEqual(expectedValue1, actualValue1);

                return new WeakReference(actualInstance);
            });

            var reference = runIsolated();

            GC.Collect(0, GCCollectionMode.Forced);
            GC.WaitForFullGCComplete();

            Assert.IsFalse(reference.IsAlive, "Instance reference should have been garbage collected");

            // NamedParameter should be used because instance needs to be created
            var actualInstance2 = _container.Resolve(expectedType,
                new NamedParameter<int>("value", expectedValue2)) as ITestService;

            Assert.IsNotNull(actualInstance2);
            Assert.IsInstanceOfType(actualInstance2, expectedType);

// ReSharper disable PossibleNullReferenceException
            var actualValue2 = actualInstance2.Value;
// ReSharper restore PossibleNullReferenceException

            Assert.AreEqual(expectedValue2, actualValue2);
        }

        [TestMethod]
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

            Assert.IsNotNull(actualInstance);
            Assert.IsInstanceOfType(actualInstance, expectedType);

            // ReSharper disable PossibleNullReferenceException
            var actualValue1 = actualInstance.Value;
            // ReSharper restore PossibleNullReferenceException

            Assert.AreEqual(expectedValue1, actualValue1);

            // NamedParameter will be ignored cause no instance needs to be created in same thread
            var actualInstance2 = _container.Resolve<TestService>(
                        new NamedParameter<int>("value", 300)) as ITestService;

            Assert.IsNotNull(actualInstance2);
            Assert.IsInstanceOfType(actualInstance2, expectedType);

            Assert.AreSame(actualInstance, actualInstance2);

            var actualValue2 = actualInstance2.Value;

            Assert.AreEqual(expectedValue2, actualValue2);
        }

        [TestMethod]
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

            Assert.IsNotNull(actualInstance);
            Assert.IsInstanceOfType(actualInstance, expectedType);

// ReSharper disable PossibleNullReferenceException
            var actualValue1 = actualInstance.Value;
// ReSharper restore PossibleNullReferenceException

            Assert.AreEqual(expectedValue1, actualValue1);

            // NamedParameter will be ignored cause no instance needs to be created in same thread
            var actualInstance2 = _container.Resolve(expectedType,
                        new NamedParameter<int>("value", 300)) as ITestService;

            Assert.IsNotNull(actualInstance2);
            Assert.IsInstanceOfType(actualInstance2, expectedType);

            Assert.AreSame(actualInstance, actualInstance2);

// ReSharper disable PossibleNullReferenceException
            var actualValue2 = actualInstance2.Value;
// ReSharper restore PossibleNullReferenceException

            Assert.AreEqual(expectedValue2, actualValue2);
        }

        [TestMethod]
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

            Assert.IsNotNull(actualInstance);
            Assert.IsInstanceOfType(actualInstance, expectedType);

// ReSharper disable PossibleNullReferenceException
            var actualValue1 = actualInstance.Value;
// ReSharper restore PossibleNullReferenceException

            Assert.AreEqual(expectedValue1, actualValue1);

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
                Assert.Fail("Timeout wating for thread");

            Assert.IsNotNull(actualInstance2);
            Assert.IsInstanceOfType(actualInstance2, expectedType);

            Assert.AreNotSame(actualInstance, actualInstance2);

            var actualValue2 = actualInstance2.Value;

            Assert.AreEqual(expectedValue2, actualValue2);
        }

        [TestMethod]
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

            Assert.IsNotNull(actualInstance);
            Assert.IsInstanceOfType(actualInstance, expectedType);

// ReSharper disable PossibleNullReferenceException
            var actualValue1 = actualInstance.Value;
// ReSharper restore PossibleNullReferenceException

            Assert.AreEqual(expectedValue1, actualValue1);

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
                Assert.Fail("Timeout wating for thread");

            Assert.IsNotNull(actualInstance2);
            Assert.IsInstanceOfType(actualInstance2, expectedType);

            Assert.AreNotSame(actualInstance, actualInstance2);

            var actualValue2 = actualInstance2.Value;

            Assert.AreEqual(expectedValue2, actualValue2);
        }

        [TestMethod]
        public void verify_container_dispose()
        {
            TestService actualInstance = null;

            var runIsolated = new Func<WeakReference>(() =>
            {
                using (var container = new Wayne.Payment.Platform.Lite.LiteContainer())
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

            Assert.IsFalse(reference.IsAlive, "Container should have been garbage collected");

            Assert.IsNotNull(actualInstance);

            Assert.IsTrue(actualInstance.IsDisposed);
        }
    }
}
