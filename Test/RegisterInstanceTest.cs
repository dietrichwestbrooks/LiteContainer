using System;
using System.Linq;
using Microsoft.Practices.ServiceLocation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wayne.Payment.Platform.Lite;

// ReSharper disable ConvertToConstant.Local
namespace LiteContainer.Test
{
    /// <summary>
    /// Summary description for InstanceTest
    /// </summary>
    [TestClass]
    public class RegisterInstanceTest
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
        // public static void TestClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void TestClassCleanup() { }
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
        public void verify_unnamed_registered_instance()
        {
            var expectedType = typeof(TestService);
            var expectedRegCount = 1;

            var expectedInstance = new TestService();

            _container.Register(expectedInstance);

            var actualRegCount = _container.Registrations.Count();

            Assert.AreEqual(expectedRegCount, actualRegCount);

            var actualType = _container.Registrations.First(r => r.ServiceType == expectedType).ServiceType;

            Assert.AreEqual(expectedType, actualType);
        }

        [TestMethod]
        public void verify_unnamed_registered_interface()
        {
            var expectedType = typeof(ITestService);
            var expectedRegCount = 1;

            var expectedInstance = new TestService();

            var registration = _container.Register(expectedInstance)
                .As(expectedType);

            var actualRegCount = registration.Registrations.Count();

            Assert.AreEqual(expectedRegCount, actualRegCount);

            var actualType = registration.Registrations[expectedType].ServiceType;

            Assert.AreEqual(expectedType, actualType);
        }

        [TestMethod]
        public void resolve_unnamed_instance()
        {
            var expectedParamValue = 100;

            var expectedInstance = new TestParameter(expectedParamValue);

            _container.Register(expectedInstance);

            var actualInstance = _container.Resolve<TestParameter>();

            Assert.AreSame(expectedInstance, actualInstance);

            var actualParamValue = actualInstance.Value;

            Assert.AreEqual(expectedParamValue, actualParamValue);
        }

        [TestMethod]
        public void resolve_unnamed_instance_for_interface()
        {
            var expectedParamValue = 100;

            var expectedInstance = new TestParameter(expectedParamValue);

            _container.Register(expectedInstance)
                .As<ITestParameter>();

            var actualInstance = _container.Resolve<ITestParameter>();

            Assert.AreSame(expectedInstance, actualInstance);

            var actualParamValue = actualInstance.Value;

            Assert.AreEqual(expectedParamValue, actualParamValue);
        }

        [TestMethod]
        public void resolve_unnamed_instance_for_multiple_interfaces()
        {
            var expectedParamValue = 100;
            var expectedParamName = "Param1";

            var expectedInstance = new TestParameter(expectedParamValue) { Name = expectedParamName };

            _container.Register(expectedInstance)
                .As<ITestParameterValue>()
                .As<ITestParameterName>();

            var actualInstance = _container.Resolve<ITestParameterValue>();

            Assert.AreSame(expectedInstance, actualInstance);

            var actualParamValue = actualInstance.Value;

            Assert.AreEqual(expectedParamValue, actualParamValue);

            var actualInstance2 = _container.Resolve<ITestParameterName>();

            Assert.AreSame(expectedInstance, actualInstance2);

            var actualParamName = actualInstance2.Name;

            Assert.AreEqual(expectedParamName, actualParamName);
        }

        [TestMethod]
        [ExpectedException(typeof(ActivationException))]
        public void resolve_unnamed_instance_for_unregistered_interface()
        {
            var expectedInstance = new TestParameter();

            _container.Register(expectedInstance);

            _container.Resolve<ITestParameter>();
        }

        [TestMethod]
        public void resolve_unnamed_instance_overwrite_registered_type()
        {
            Type registeredType = typeof(ITestProperty);
            Type overwrittenType = typeof(TestPropertyNoDefaultConstructor);

            var expectedInstance = new TestProperty();

            _container.Register(overwrittenType)
                .Parameters(typeof(int))
                .As(registeredType);

            var overwrittenInstance = _container.Resolve(registeredType, new NamedParameter<int>("value", 1));

            Assert.IsNotNull(overwrittenInstance);

            Assert.IsInstanceOfType(overwrittenInstance, overwrittenType);

            _container.Register(expectedInstance)
                .As(registeredType);

            var actualInstance = _container.Resolve(registeredType);

            Assert.IsNotNull(actualInstance);

            Assert.AreSame(expectedInstance, actualInstance);
        }

        [TestMethod]
        public void resolve_unnamed_instance_overwrite_registered_instance()
        {
            Type registeredType = typeof(ITestProperty);
            Type overwrittenType = typeof(TestPropertyNoDefaultConstructor);

            var expectedInstance = new TestProperty();

            _container.Register(new TestPropertyNoDefaultConstructor(1))
                .As(registeredType);

            var overwrittenInstance = _container.Resolve(registeredType);

            Assert.IsNotNull(overwrittenInstance);

            Assert.IsInstanceOfType(overwrittenInstance, overwrittenType);

            _container.Register(expectedInstance)
                .As(registeredType);

            var actualInstance = _container.Resolve(registeredType);

            Assert.IsNotNull(actualInstance);

            Assert.AreSame(expectedInstance, actualInstance);
        }

        [TestMethod]
        [ExpectedException(typeof(ActivationException))]
        public void resolve_named_instance_when_incorrect_name()
        {
            var expectedName = "test";
            var badName = "badName";

            var expectedInstance = new TestParameter();

            _container.Register(expectedInstance, expectedName);

            _container.Resolve<TestParameter>(badName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void register_unnamed_instance_null_service()
        {
            var expectedInstance = (TestProperty)null;

            // ReSharper disable ExpressionIsAlwaysNull
            _container.Register(expectedInstance);
            // ReSharper restore ExpressionIsAlwaysNull
        }

        [TestMethod]
        public void resolve_named_instance()
        {
            var expectedName = "test";
            var expectedParamValue = 100;

            var expectedInstance = new TestParameter(expectedParamValue);

            _container.Register(expectedInstance, expectedName);

            var actualInstance = _container.Resolve<TestParameter>(expectedName);

            Assert.AreSame(expectedInstance, actualInstance);

            var actualParamValue = actualInstance.Value;

            Assert.AreEqual(expectedParamValue, actualParamValue);
        }

        [TestMethod]
        public void resolve_named_instance_for_interface()
        {
            var expectedName = "test";
            var expectedParamValue = 100;

            var expectedInstance = new TestParameter(expectedParamValue);

            _container.Register(expectedInstance, expectedName)
                .As<ITestParameter>();

            var actualInstance = _container.Resolve<ITestParameter>(expectedName);

            Assert.AreSame(expectedInstance, actualInstance);

            var actualParamValue = actualInstance.Value;

            Assert.AreEqual(expectedParamValue, actualParamValue);
        }

        [TestMethod]
        public void resolve_named_instance_for_multiple_interfaces()
        {
            var expectedName = "test";
            var expectedParamValue = 100;
            var expectedParamName = "Param1";

            var expectedInstance = new TestParameter(expectedParamName, expectedParamValue);

            _container.Register(expectedInstance, expectedName)
                .As<ITestParameterValue>()
                .As<ITestParameterName>();

            var actualInstance = _container.Resolve<ITestParameterValue>(expectedName);

            Assert.AreSame(expectedInstance, actualInstance);

            var actualParamValue = actualInstance.Value;

            Assert.AreEqual(expectedParamValue, actualParamValue);

            var resolvedInstance2 = _container.Resolve<ITestParameterName>(expectedName);

            Assert.IsTrue(expectedInstance == resolvedInstance2);

            var actualParamName = resolvedInstance2.Name;

            Assert.AreEqual(expectedParamName, actualParamName);
        }

        [TestMethod]
        [ExpectedException(typeof(ActivationException))]
        public void resolve_named_instance_for_unregistered_interface()
        {
            var expectedName = "test";

            var expectedInstance = new TestParameter();

            _container.Register(expectedInstance, expectedName);

            _container.Resolve<ITestParameter>();
        }

        [TestMethod]
        public void resolve_named_instance_overwrite_registered_type()
        {
            var expectedName = "test";
            Type registeredType = typeof(ITestProperty);
            Type overwrittenType = typeof(TestPropertyNoDefaultConstructor);

            var expectedInstance = new TestProperty();

            _container.Register(overwrittenType, expectedName)
                .Parameters(typeof(int))
                .As(registeredType);

            var overwrittenInstance = _container.Resolve(registeredType, expectedName,
                new NamedParameter<int>("value", 1));

            Assert.IsNotNull(overwrittenInstance);

            Assert.IsInstanceOfType(overwrittenInstance, overwrittenType);

            _container.Register(expectedInstance, expectedName)
                .As(registeredType);

            var actualInstance = _container.Resolve(registeredType, expectedName);

            Assert.IsNotNull(actualInstance);

            Assert.AreSame(expectedInstance, actualInstance);
        }

        [TestMethod]
        public void resolve_named_instance_overwrite_registered_instance()
        {
            var expectedName = "test";
            Type registeredType = typeof(ITestProperty);
            Type overwrittenType = typeof(TestPropertyNoDefaultConstructor);

            var expectedInstance = new TestProperty();

            _container.Register(new TestPropertyNoDefaultConstructor(1), expectedName)
                .As(registeredType);

            var overwrittenInstance = _container.Resolve(registeredType, expectedName);

            Assert.IsNotNull(overwrittenInstance);

            Assert.IsInstanceOfType(overwrittenInstance, overwrittenType);

            _container.Register(expectedInstance, expectedName)
                .As(registeredType);

            var actualInstance = _container.Resolve(registeredType, expectedName);

            Assert.IsNotNull(actualInstance);

            Assert.AreSame(expectedInstance, actualInstance);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void register_named_instance_null_name()
        {
            var expectedName = (string)null;
            var expectedInstance = new TestProperty();

            // ReSharper disable ExpressionIsAlwaysNull
            _container.Register(expectedInstance, expectedName);
            // ReSharper restore ExpressionIsAlwaysNull
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void register_named_instance_null_service()
        {
            var expectedName = "test";
            var expectedInstance = (TestProperty)null;

            // ReSharper disable ExpressionIsAlwaysNull
            _container.Register(expectedInstance, expectedName);
            // ReSharper restore ExpressionIsAlwaysNull
        }

        [TestMethod]
        public void resolve_all_named_instances()
        {
            var expectedName = "test";
            var registeredName2 = "test2";
            var expectedType = typeof(TestProperty);
            var expectedRegCount = 3;
            var expectedResolveCount = 2;
            var expectedValue = 1;
            var expectedValue2 = 2;
            var expectedValue3 = 3;

            var expectedInstance = new TestProperty(expectedValue);
            var expectedInstance2 = new TestProperty(expectedValue2);
            var unamedInstance = new TestProperty(expectedValue3);

            _container.Register(expectedInstance, expectedName);

            _container.Register(expectedInstance2, registeredName2);

            _container.Register(unamedInstance);

            Assert.AreEqual(expectedRegCount, _container.Registrations.Count());

            var actualInstances = _container.ResolveAll(expectedType).Cast<TestProperty>().ToArray();

            Assert.IsNotNull(actualInstances);

            Assert.AreEqual(expectedResolveCount, actualInstances.Count());

            var actualInstance = actualInstances.FirstOrDefault(p => p.Value == expectedValue);

            Assert.AreSame(expectedInstance, actualInstance);

            var actualInstance2 = actualInstances.FirstOrDefault(p => p.Value == expectedValue2);

            Assert.AreSame(expectedInstance2, actualInstance2);

            var actualInstance3 = actualInstances.FirstOrDefault(p => p.Value == expectedValue3);

            Assert.IsNull(actualInstance3);
        }

        [TestMethod]
        public void resolve_all_named_instances_generic()
        {
            var expectedName = "test";
            var registeredName2 = "test2";
            var expectedRegCount = 3;
            var expectedResolveCount = 2;
            var expectedValue = 1;
            var expectedValue2 = 2;
            var expectedValue3 = 3;

            var expectedInstance = new TestProperty(expectedValue);
            var expectedInstance2 = new TestProperty(expectedValue2);
            var unamedInstance = new TestProperty(expectedValue3);

            _container.Register(expectedInstance, expectedName);

            _container.Register(expectedInstance2, registeredName2);

            _container.Register(unamedInstance);

            Assert.AreEqual(expectedRegCount, _container.Registrations.Count());

            var actualInstances = _container.ResolveAll<TestProperty>().ToArray();

            Assert.IsNotNull(actualInstances);

            Assert.AreEqual(expectedResolveCount, actualInstances.Count());

            var actualInstance = actualInstances.FirstOrDefault(p => p.Value == expectedValue);

            Assert.AreSame(expectedInstance, actualInstance);

            var actualInstance2 = actualInstances.FirstOrDefault(p => p.Value == expectedValue2);

            Assert.AreSame(expectedInstance2, actualInstance2);

            var actualInstance3 = actualInstances.FirstOrDefault(p => p.Value == expectedValue3);

            Assert.IsNull(actualInstance3);
        }

        [TestMethod]
        public void resolve_all_named_instances_and_types()
        {
            var expectedName = "test";
            var registeredName2 = "test2";
            var expectedType = typeof(TestProperty);
            var expectedRegCount = 4;
            var expectedResolveCount = 2;

            var expectedInstance = new TestProperty();
            var unamedInstance = new TestProperty();

            _container.Register(expectedInstance, expectedName);

            _container.Register(expectedType, registeredName2);

            _container.Register(unamedInstance);

            _container.Register(expectedType);

            Assert.AreEqual(expectedRegCount, _container.Registrations.Count());

            var actualInstances = _container.ResolveAll(expectedType).Cast<TestProperty>().ToArray();

            Assert.IsNotNull(actualInstances);

            Assert.AreEqual(expectedResolveCount, actualInstances.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void resolve_all_null_service_type()
        {
            var expectedType = (Type)null;

// ReSharper disable ExpressionIsAlwaysNull
            _container.ResolveAll(expectedType);
// ReSharper restore ExpressionIsAlwaysNull
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void register_instance_for_incompatible_interface()
        {
            var expectedInstance = new TestService();

            _container.Register(expectedInstance)
                .As<ITestProperty>();
        }

        [TestMethod]
        public void verify_named_registered_instance()
        {
            var expectedName = "test";
            var expectedType = typeof(TestService);
            var expectedRegCount = 1;

            var expectedInstance = new TestService();

            _container.Register(expectedInstance, expectedName);

            var actualRegCount = _container.Registrations.Count();

            Assert.AreEqual(expectedRegCount, actualRegCount);

            var actualType = _container.Registrations.First(r => r.ServiceType == expectedType).ServiceType;

            Assert.AreEqual(expectedType, actualType);

            var actualName = _container.Registrations.First(r => r.ServiceType == expectedType).Name;

            Assert.AreEqual(expectedName, actualName);
        }

        [TestMethod]
        public void verify_named_registered_interface()
        {
            var expectedName = "test";
            var expectedType = typeof(ITestService);
            var expectedRegCount = 1;

            var expectedInstance = new TestService();

            var registration = _container.Register(expectedInstance, expectedName)
                .As(expectedType);

            var actualRegCount = registration.Registrations.Count();

            Assert.AreEqual(expectedRegCount, actualRegCount);

            var actualType = registration.Registrations[expectedType].ServiceType;

            Assert.AreEqual(expectedType, actualType);

            var actualName = registration.Registrations[expectedType].Name;

            Assert.AreEqual(expectedName, actualName);
        }
    }
}
