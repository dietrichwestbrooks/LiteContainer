using System;
using System.Linq;
using Microsoft.Practices.ServiceLocation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wayne.Payment.Platform.Lite;

// ReSharper disable ConvertToConstant.Local
namespace LiteContainer.Test
{
    /// <summary>
    /// Summary description for RegisterTypeTest
    /// </summary>
    [TestClass]
    public class RegisterTypeTest
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
        public void verify_unnamed_registered_type()
        {
            var expectedType = typeof(TestService);
            var expectedRegCount = 1;

            _container.Register(expectedType);

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

            var registration = _container.Register(expectedType)
                .As(expectedType);

            var actualRegCount = registration.Registrations.Count();

            Assert.AreEqual(expectedRegCount, actualRegCount);

            var actualType = registration.Registrations[expectedType].ServiceType;

            Assert.AreEqual(expectedType, actualType);
        }

        [TestMethod]
        public void resolve_unnamed_type()
        {
            Type expectedType = typeof(TestParameter);

            _container.Register(expectedType);

            var actualInstance = _container.Resolve(expectedType);

            Assert.IsNotNull(actualInstance);

            Assert.IsInstanceOfType(actualInstance, expectedType);
        }

        [TestMethod]
        public void resolve_unnamed_type_using_generic()
        {
            Type expectedType = typeof(TestParameter);

            _container.Register<TestParameter>();

            var actualInstance = _container.Resolve<TestParameter>();

            Assert.IsNotNull(actualInstance);

            Assert.IsInstanceOfType(actualInstance, expectedType);
        }

        [TestMethod]
        public void resolve_unnamed_type_for_interface()
        {
            Type expectedType = typeof(ITestParameter);

            _container.Register(typeof(TestParameter))
                .As(expectedType);

            var actualInstance = _container.Resolve(expectedType);

            Assert.IsNotNull(actualInstance);

            Assert.IsInstanceOfType(actualInstance, expectedType);
        }

        [TestMethod]
        public void resolve_unnamed_type_for_interface_using_generic()
        {
            Type expectedType = typeof(ITestParameter);

            _container.Register(typeof(TestParameter))
                .As<ITestParameter>();

            var actualInstance = _container.Resolve<ITestParameter>();

            Assert.IsNotNull(actualInstance);

            Assert.IsInstanceOfType(actualInstance, expectedType);
        }

        [TestMethod]
        public void resolve_unnamed_type_for_multiple_interfaces()
        {
            Type expectedType = typeof(ITestParameterName);
            Type expectedType2 = typeof(ITestParameterValue);

            var expectedRegCount = 3;

            _container.Register(typeof(TestParameter))
                .As(expectedType)
                .As(expectedType2);

            var actualRegCount = _container.Registrations.Count();

            Assert.AreEqual(expectedRegCount, actualRegCount);

            var actualInstance = _container.Resolve(expectedType);

            Assert.IsNotNull(actualInstance);

            Assert.IsInstanceOfType(actualInstance, expectedType);

            var actualInstance2 = _container.Resolve(expectedType2);

            Assert.IsNotNull(actualInstance2);

            Assert.IsInstanceOfType(actualInstance2, expectedType2);
        }

        [TestMethod]
        public void resolve_unnamed_type_with_named_resolve_parameter()
        {
            Type expectedType = typeof(TestParameter);
            var expectedParamValue = 100;

            _container.Register(expectedType)
                .Parameters(typeof(int));

            var resolvedInstance = (TestParameter)_container.Resolve(expectedType,
                new NamedParameter<int>("value", expectedParamValue));

            Assert.IsNotNull(resolvedInstance);

            Assert.IsInstanceOfType(resolvedInstance, expectedType);

            var actualValue = resolvedInstance.Value;

            Assert.AreEqual(expectedParamValue, actualValue);
        }

        [TestMethod]
        public void resolve_unnamed_type_with_typed_resolve_parameter()
        {
            Type expectedType = typeof(TestParameter);
            var expectedParamName = "Param1";

            _container.Register(expectedType)
                .Parameters(typeof(string));

            var resolvedInstance = (TestParameter)_container.Resolve(expectedType,
                new TypedParameter<string>(expectedParamName));

            Assert.IsNotNull(resolvedInstance);

            Assert.IsInstanceOfType(resolvedInstance, expectedType);

            var actualParamName = resolvedInstance.Name;

            Assert.AreEqual(expectedParamName, actualParamName);
        }

        [TestMethod]
        public void resolve_unnamed_type_with_mixed_resolve_parameters()
        {
            Type expectedType = typeof(TestParameter);
            var expectedParamValue = 100;
            var expectedParamName = "Param1";

            _container.Register(expectedType)
                .Parameters(typeof(string), typeof(int));

            var resolvedInstance = (TestParameter)_container.Resolve(expectedType,
                new TypedParameter<string>(expectedParamName),
                new NamedParameter<int>("value", expectedParamValue));

            Assert.IsNotNull(resolvedInstance);

            Assert.IsInstanceOfType(resolvedInstance, expectedType);

            var actualParamName = resolvedInstance.Name;

            Assert.AreEqual(expectedParamName, actualParamName);

            var actualValue = resolvedInstance.Value;

            Assert.AreEqual(expectedParamValue, actualValue);
        }

        [TestMethod]
        public void resolve_unnamed_type_with_multiple_typed_resolve_parameters()
        {
            Type expectedType = typeof(TestParameter);
            var expectedParamValue = 100;
            var expectedParamName = "Param1";

            _container.Register(expectedType)
                .Parameters(typeof(string), typeof(int));

            var resolvedInstance = (TestParameter)_container.Resolve(expectedType,
                new TypedParameter<string>(expectedParamName),
                new TypedParameter<int>(expectedParamValue));

            Assert.IsNotNull(resolvedInstance);

            Assert.IsInstanceOfType(resolvedInstance, expectedType);

            var actualParamName = resolvedInstance.Name;

            Assert.AreEqual(expectedParamName, actualParamName);

            var actualValue = resolvedInstance.Value;

            Assert.AreEqual(expectedParamValue, actualValue);
        }

        [TestMethod]
        public void resolve_unnamed_type_with_multiple_named_resolve_parameters()
        {
            Type expectedType = typeof(TestParameter);
            var expectedParamValue = 100;
            var expectedParamName = "Param1";

            _container.Register(expectedType)
                .Parameters(typeof(string), typeof(int));

            var resolvedInstance = (TestParameter)_container.Resolve(expectedType,
                new NamedParameter<string>("name", expectedParamName),
                new NamedParameter<int>("value", expectedParamValue));

            Assert.IsNotNull(resolvedInstance);

            Assert.IsInstanceOfType(resolvedInstance, expectedType);

            var actualParamName = resolvedInstance.Name;

            Assert.AreEqual(expectedParamName, actualParamName);

            var actualValue = resolvedInstance.Value;

            Assert.AreEqual(expectedParamValue, actualValue);
        }

        [TestMethod]
        public void resolve_unnamed_type_with_ordered_resolve_parameters()
        {
            Type expectedType = typeof(TestParameter);
            var expectedParamValue = 100;
            var expectedParamName = "Param1";

            _container.Register(expectedType)
                .Parameters(typeof(string), typeof(int));

            var resolvedInstance = (TestParameter)_container.Resolve(expectedType,
                new OrderedParameters(expectedParamName, expectedParamValue));

            Assert.IsNotNull(resolvedInstance);

            Assert.IsInstanceOfType(resolvedInstance, expectedType);

            var actualParamName = resolvedInstance.Name;

            Assert.AreEqual(expectedParamName, actualParamName);

            var actualValue = resolvedInstance.Value;

            Assert.AreEqual(expectedParamValue, actualValue);
        }

        [TestMethod]
        public void resolve_unnamed_type_with_ordered_resolve_parameters_and_registered_interface()
        {
            Type expectedType = typeof(TestService);
            var expectedParamType = typeof (ITestParameter);
            var expectedParamValue = 100;

            var expectedParamInstance = new TestParameter();

            _container.Register(expectedParamInstance)
                .As(expectedParamType);

            _container.Register(expectedType)
                .Parameters(typeof(int), typeof(ITestParameter));

            var resolvedInstance = _container.Resolve(expectedType,
                new OrderedParameters(expectedParamValue)) as TestService;

            Assert.IsNotNull(resolvedInstance);

            Assert.IsInstanceOfType(resolvedInstance, expectedType);

// ReSharper disable PossibleNullReferenceException
            var actualParamValue = resolvedInstance.Value;
// ReSharper restore PossibleNullReferenceException

            Assert.AreEqual(expectedParamValue, actualParamValue);

            var acutalParamInstance = resolvedInstance.Parameter;

            Assert.IsNotNull(acutalParamInstance);

            Assert.AreSame(expectedParamInstance, acutalParamInstance);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void inject_property_by_name_when_property_not_of_interface_type()
        {
            Type expectedType = typeof(TestService);
            var expectedPropName = "Value";

            _container.Register(expectedType)
                .Property(expectedPropName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void inject_property_by_name_with_non_existing_property_name()
        {
            Type expectedType = typeof(TestService);
            var expectedPropName = "NonExistingProperty";

            _container.Register(expectedType)
                .Property(expectedPropName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void inject_property_by_type_with_non_interface_type()
        {
            Type expectedType = typeof(TestService);
            var expectedPropType = typeof(int);

            _container.Register(expectedType)
                .Property(expectedPropType);
        }

        [TestMethod]
        public void inject_property_by_name_when_type_missing()
        {
            Type expectedType = typeof(TestService);
            var expectedPropName = "TestProperty";

            _container.Register(expectedType)
                .Property(expectedPropName);

            var resolvedInstance = (TestService)_container.Resolve(expectedType);

            Assert.IsNotNull(resolvedInstance);

            Assert.IsInstanceOfType(resolvedInstance, expectedType);

            Assert.IsNull(resolvedInstance.TestProperty);
        }

        [TestMethod]
        public void inject_property_by_name()
        {
            Type expectedType = typeof(TestService);
            var expectedPropType = typeof(ITestProperty);
            var expectedPropName = "TestProperty";

            var expectedPropInstance = new TestProperty();

            _container.Register(expectedPropInstance)
                .As(expectedPropType);

            _container.Register(expectedType)
                .Property(expectedPropName);

            var resolvedInstance = (TestService)_container.Resolve(expectedType);

            Assert.IsNotNull(resolvedInstance);

            Assert.IsInstanceOfType(resolvedInstance, expectedType);

            var acutalPropInstance = resolvedInstance.TestProperty;

            Assert.IsNotNull(acutalPropInstance);

            Assert.AreSame(expectedPropInstance, acutalPropInstance);
        }

        [TestMethod]
        public void inject_property_by_type()
        {
            Type expectedType = typeof(TestService);
            var expectedPropType = typeof(ITestProperty);

            var expectedPropInstance = new TestProperty();

            _container.Register(expectedPropInstance)
                .As(expectedPropType);

            _container.Register(expectedType)
                .Property(expectedPropType);

            var resolvedInstance = (TestService)_container.Resolve(expectedType);

            Assert.IsNotNull(resolvedInstance);

            Assert.IsInstanceOfType(resolvedInstance, expectedType);

            var acutalPropInstance = resolvedInstance.TestProperty;

            Assert.IsNotNull(acutalPropInstance);

            Assert.AreSame(expectedPropInstance, acutalPropInstance);
        }

        [TestMethod]
        public void inject_multiple_properties_by_type()
        {
            Type expectedType = typeof(TestService);
            var expectedPropType = typeof(ITestProperty);

            var expectedPropInstance = new TestProperty();

            _container.Register(expectedPropInstance)
                .As(expectedPropType);

            _container.Register(expectedType)
                .Property(expectedPropType);

            var resolvedInstance = (TestService)_container.Resolve(expectedType);

            Assert.IsNotNull(resolvedInstance);

            Assert.IsInstanceOfType(resolvedInstance, expectedType);

            var acutalPropInstance = resolvedInstance.TestProperty;

            Assert.IsNotNull(acutalPropInstance);

            Assert.AreSame(expectedPropInstance, acutalPropInstance);

            var acutalPropInstance2 = resolvedInstance.TestProperty2;

            Assert.IsNotNull(acutalPropInstance2);

            Assert.AreSame(expectedPropInstance, acutalPropInstance2);
        }

        [TestMethod]
        public void resolve_unnamed_type_with_registered_interface_parameter()
        {
            Type expectedType = typeof(TestService);
            var expectedParamType = typeof(ITestParameter);

            var expectedParamInstance = new TestParameter();

            _container.Register(expectedParamInstance)
                .As(expectedParamType);

            _container.Register(expectedType)
                .Parameters(expectedParamType);

            var resolvedInstance = _container.Resolve(expectedType) as TestService;

            Assert.IsNotNull(resolvedInstance);

            Assert.IsInstanceOfType(resolvedInstance, expectedType);

// ReSharper disable PossibleNullReferenceException
            var actualParamInstance = resolvedInstance.Parameter;
// ReSharper restore PossibleNullReferenceException

            Assert.IsNotNull(actualParamInstance);

            Assert.AreSame(expectedParamInstance, actualParamInstance);
        }

        [TestMethod]
        [ExpectedException(typeof(ActivationException))]
        public void resolve_unnamed_type_with_unregistered_interface_parameter()
        {
            Type expectedType = typeof(TestService);
            var expectedParamType = typeof(ITestParameter);

            _container.Register(expectedType)
                .Parameters(expectedParamType);

            _container.Resolve(expectedType);
        }

        [TestMethod]
        [ExpectedException(typeof(ActivationException))]
        public void resolve_unnamed_type_with_invalid_constructor()
        {
            Type expectedType = typeof(TestParameter);

            _container.Register(expectedType)
                .Parameters(typeof(bool));

            _container.Resolve(expectedType);
        }

        [TestMethod]
        public void resolve_unnamed_type_overwrite_registered_type()
        {
            Type registeredType = typeof(ITestProperty);
            Type expectedType = typeof(TestProperty);
            Type overwrittenType = typeof(TestPropertyNoDefaultConstructor);

            _container.Register(overwrittenType)
                .Parameters(typeof(int))
                .As(registeredType);

            var overwrittenInstance = _container.Resolve(registeredType, new NamedParameter<int>("value", 1));

            Assert.IsNotNull(overwrittenInstance);

            Assert.IsInstanceOfType(overwrittenInstance, overwrittenType);

            _container.Register(expectedType)
                .As(registeredType);

            var actualInstance = _container.Resolve(registeredType);

            Assert.IsNotNull(actualInstance);

            Assert.IsInstanceOfType(actualInstance, expectedType);
        }

        [TestMethod]
        public void resolve_unnamed_type_overwrite_registered_instance()
        {
            Type registeredType = typeof(ITestProperty);
            Type expectedType = typeof(TestProperty);
            Type overwrittenType = typeof(TestPropertyNoDefaultConstructor);

            _container.Register(new TestPropertyNoDefaultConstructor(1))
                .As(registeredType);

            var overwrittenInstance = _container.Resolve(registeredType);

            Assert.IsNotNull(overwrittenInstance);

            Assert.IsInstanceOfType(overwrittenInstance, overwrittenType);

            _container.Register(expectedType)
                .As(registeredType);

            var actualInstance = _container.Resolve(registeredType);

            Assert.IsNotNull(actualInstance);

            Assert.IsInstanceOfType(actualInstance, expectedType);
        }

        [TestMethod]
        [ExpectedException(typeof(ActivationException))]
        public void resolve_unnamed_type_with_no_default_constructor()
        {
            Type expectedType = typeof(TestPropertyNoDefaultConstructor);

            _container.Register(expectedType);

            _container.Resolve(expectedType);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void register_unnamed_type_null_service_type()
        {
            Type expectedType = null;

            // ReSharper disable ExpressionIsAlwaysNull
            _container.Register(expectedType);
            // ReSharper restore ExpressionIsAlwaysNull
        }

        [TestMethod]
        public void resolve_named_type()
        {
            var expectedName = "test";
            Type expectedType = typeof(TestParameter);

            _container.Register(expectedType, expectedName);

            var actualInstance = _container.Resolve(expectedType, expectedName);

            Assert.IsNotNull(actualInstance);

            Assert.IsInstanceOfType(actualInstance, expectedType);
        }

        [TestMethod]
        public void resolve_named_type_using_generic()
        {
            var expectedName = "test";
            Type expectedType = typeof(TestParameter);

            _container.Register<TestParameter>(expectedName);

            var actualInstance = _container.Resolve<TestParameter>(expectedName);

            Assert.IsNotNull(actualInstance);

            Assert.IsInstanceOfType(actualInstance, expectedType);
        }

        [TestMethod]
        public void resolve_named_type_for_interface()
        {
            var expectedName = "test";
            Type expectedType = typeof(ITestParameter);

            _container.Register(typeof(TestParameter), expectedName)
                .As(expectedType);

            var actualInstance = _container.Resolve(expectedType, expectedName);

            Assert.IsNotNull(actualInstance);

            Assert.IsInstanceOfType(actualInstance, expectedType);
        }

        [TestMethod]
        public void resolve_named_type_for_interface_using_generic()
        {
            var expectedName = "test";
            Type expectedType = typeof(ITestParameter);

            _container.Register(typeof(TestParameter), expectedName)
                .As<ITestParameter>();

            var actualInstance = _container.Resolve<ITestParameter>(expectedName);

            Assert.IsNotNull(actualInstance);

            Assert.IsInstanceOfType(actualInstance, expectedType);
        }

        [TestMethod]
        public void resolve_named_type_for_multiple_interfaces()
        {
            var expectedName = "test";
            Type expectedType = typeof(ITestParameterName);
            Type expectedType2 = typeof(ITestParameterValue);

            var expectedRegCount = 3;

            _container.Register(typeof(TestParameter), expectedName)
                .As(expectedType)
                .As(expectedType2);

            var actualRegCount = _container.Registrations.Count();

            Assert.AreEqual(expectedRegCount, actualRegCount);

            var actualInstance = _container.Resolve(expectedType, expectedName);

            Assert.IsNotNull(actualInstance);

            Assert.IsInstanceOfType(actualInstance, expectedType);

            var actualInstance2 = _container.Resolve(expectedType2, expectedName);

            Assert.IsNotNull(actualInstance2);

            Assert.IsInstanceOfType(actualInstance2, expectedType2);
        }

        [TestMethod]
        public void resolve_named_type_with_named_resolve_parameter()
        {
            var expectedName = "test";
            Type expectedType = typeof(TestParameter);
            var expectedParamValue = 100;

            _container.Register(expectedType, expectedName)
                .Parameters(typeof(int));

            var resolvedInstance = (TestParameter)_container.Resolve(expectedType, expectedName,
                new NamedParameter<int>("value", expectedParamValue));

            Assert.IsNotNull(resolvedInstance);

            Assert.IsInstanceOfType(resolvedInstance, expectedType);

            var actualValue = resolvedInstance.Value;

            Assert.AreEqual(expectedParamValue, actualValue);
        }

        [TestMethod]
        public void resolve_named_type_with_typed_resolve_parameter()
        {
            var expectedName = "test";
            Type expectedType = typeof(TestParameter);
            var expectedParamName = "Param1";

            _container.Register(expectedType, expectedName)
                .Parameters(typeof(string));

            var resolvedInstance = (TestParameter)_container.Resolve(expectedType, expectedName,
                new TypedParameter<string>(expectedParamName));

            Assert.IsNotNull(resolvedInstance);

            Assert.IsInstanceOfType(resolvedInstance, expectedType);

            var actualParamName = resolvedInstance.Name;

            Assert.AreEqual(expectedParamName, actualParamName);
        }

        [TestMethod]
        public void resolve_named_type_with_mixed_resolve_parameters()
        {
            var expectedName = "test";
            Type expectedType = typeof(TestParameter);
            var expectedParamValue = 100;
            var expectedParamName = "Param1";

            _container.Register(expectedType, expectedName)
                .Parameters(typeof(string), typeof(int));

            var resolvedInstance = (TestParameter)_container.Resolve(expectedType, expectedName,
                new TypedParameter<string>(expectedParamName),
                new NamedParameter<int>("value", expectedParamValue));

            Assert.IsNotNull(resolvedInstance);

            Assert.IsInstanceOfType(resolvedInstance, expectedType);

            var actualParamName = resolvedInstance.Name;

            Assert.AreEqual(expectedParamName, actualParamName);

            var actualValue = resolvedInstance.Value;

            Assert.AreEqual(expectedParamValue, actualValue);
        }

        [TestMethod]
        public void resolve_named_type_with_multiple_typed_resolve_parameters()
        {
            var expectedName = "test";
            Type expectedType = typeof(TestParameter);
            var expectedParamValue = 100;
            var expectedParamName = "Param1";

            _container.Register(expectedType, expectedName)
                .Parameters(typeof(string), typeof(int));

            var resolvedInstance = (TestParameter)_container.Resolve(expectedType, expectedName,
                new TypedParameter<string>(expectedParamName),
                new TypedParameter<int>(expectedParamValue));

            Assert.IsNotNull(resolvedInstance);

            Assert.IsInstanceOfType(resolvedInstance, expectedType);

            var actualParamName = resolvedInstance.Name;

            Assert.AreEqual(expectedParamName, actualParamName);

            var actualValue = resolvedInstance.Value;

            Assert.AreEqual(expectedParamValue, actualValue);
        }

        [TestMethod]
        public void resolve_named_type_with_multiple_named_resolve_parameters()
        {
            var expectedName = "test";
            Type expectedType = typeof(TestParameter);
            var expectedParamValue = 100;
            var expectedParamName = "Param1";

            _container.Register(expectedType, expectedName)
                .Parameters(typeof(string), typeof(int));

            var resolvedInstance = (TestParameter)_container.Resolve(expectedType, expectedName,
                new NamedParameter<string>("name", expectedParamName),
                new NamedParameter<int>("value", expectedParamValue));

            Assert.IsNotNull(resolvedInstance);

            Assert.IsInstanceOfType(resolvedInstance, expectedType);

            var actualParamName = resolvedInstance.Name;

            Assert.AreEqual(expectedParamName, actualParamName);

            var actualValue = resolvedInstance.Value;

            Assert.AreEqual(expectedParamValue, actualValue);
        }

        [TestMethod]
        public void resolve_named_type_with_ordered_resolve_parameters()
        {
            var expectedName = "test";
            Type expectedType = typeof(TestParameter);
            var expectedParamValue = 100;
            var expectedParamName = "Param1";

            _container.Register(expectedType, expectedName)
                .Parameters(typeof(string), typeof(int));

            var resolvedInstance = (TestParameter)_container.Resolve(expectedType, expectedName,
                new OrderedParameters(expectedParamName, expectedParamValue));

            Assert.IsNotNull(resolvedInstance);

            Assert.IsInstanceOfType(resolvedInstance, expectedType);

            var actualParamName = resolvedInstance.Name;

            Assert.AreEqual(expectedParamName, actualParamName);

            var actualValue = resolvedInstance.Value;

            Assert.AreEqual(expectedParamValue, actualValue);
        }

        [TestMethod]
        public void resolve_named_type_with_ordered_resolve_parameters_and_registered_interface()
        {
            var expectedName = "test";
            Type expectedType = typeof(TestService);
            var expectedParamType = typeof(ITestParameter);
            var expectedParamValue = 100;

            var expectedParamInstance = new TestParameter();

            _container.Register(expectedParamInstance)
                .As(expectedParamType);

            _container.Register(expectedType, expectedName)
                .Parameters(typeof(int), typeof(ITestParameter));

            var resolvedInstance = _container.Resolve(expectedType, expectedName,
                new OrderedParameters(expectedParamValue)) as TestService;

            Assert.IsNotNull(resolvedInstance);

            Assert.IsInstanceOfType(resolvedInstance, expectedType);

// ReSharper disable PossibleNullReferenceException
            var actualParamValue = resolvedInstance.Value;
// ReSharper restore PossibleNullReferenceException

            Assert.AreEqual(expectedParamValue, actualParamValue);

            var acutalParamInstance = resolvedInstance.Parameter;

            Assert.IsNotNull(acutalParamInstance);

            Assert.AreSame(expectedParamInstance, acutalParamInstance);
        }

        [TestMethod]
        public void resolve_named_type_with_registered_interface_parameter()
        {
            var expectedName = "test";
            Type expectedType = typeof(TestService);
            var expectedParamType = typeof(ITestParameter);

            var expectedParamInstance = new TestParameter();

            _container.Register(expectedParamInstance)
                .As(expectedParamType);

            _container.Register(expectedType, expectedName)
                .Parameters(expectedParamType);

            var resolvedInstance = _container.Resolve(expectedType, expectedName) as TestService;

            Assert.IsNotNull(resolvedInstance);

            Assert.IsInstanceOfType(resolvedInstance, expectedType);

// ReSharper disable PossibleNullReferenceException
            var actualParamInstance = resolvedInstance.Parameter;
// ReSharper restore PossibleNullReferenceException

            Assert.IsNotNull(actualParamInstance);

            Assert.AreSame(expectedParamInstance, actualParamInstance);
        }

        [TestMethod]
        [ExpectedException(typeof(ActivationException))]
        public void resolve_named_type_with_unregistered_interface_parameter()
        {
            var expectedName = "test";
            Type expectedType = typeof(TestService);
            var expectedParamType = typeof(ITestParameter);

            _container.Register(expectedType, expectedName)
                .Parameters(expectedParamType);

            _container.Resolve(expectedType, expectedName);
        }

        [TestMethod]
        [ExpectedException(typeof(ActivationException))]
        public void resolve_named_type_with_invalid_constructor()
        {
            var expectedName = "test";
            Type expectedType = typeof(TestParameter);

            _container.Register(expectedType, expectedName)
                .Parameters(typeof(bool));

            _container.Resolve(expectedType, expectedName);
        }

        [TestMethod]
        [ExpectedException(typeof(ActivationException))]
        public void resolve_named_type_with_no_default_constructor()
        {
            var expectedName = "test";
            Type expectedType = typeof(TestPropertyNoDefaultConstructor);

            _container.Register(expectedType, expectedName);

            _container.Resolve(expectedType, expectedName);
        }

        [TestMethod]
        public void resolve_named_type_overwrite_registered_type()
        {
            var expectedName = "test";
            Type registeredType = typeof(ITestProperty);
            Type expectedType = typeof(TestProperty);
            Type overwrittenType = typeof(TestPropertyNoDefaultConstructor);

            _container.Register(overwrittenType, expectedName)
                .Parameters(typeof(int))
                .As(registeredType);

            var overwrittenInstance = _container.Resolve(registeredType, expectedName, new NamedParameter<int>("value", 1));

            Assert.IsNotNull(overwrittenInstance);

            Assert.IsInstanceOfType(overwrittenInstance, overwrittenType);

            _container.Register(expectedType, expectedName)
                .As(registeredType);

            var actualInstance = _container.Resolve(registeredType, expectedName);

            Assert.IsNotNull(actualInstance);

            Assert.IsInstanceOfType(actualInstance, expectedType);
        }

        [TestMethod]
        public void resolve_named_type_overwrite_registered_instance()
        {
            var expectedName = "test";
            Type registeredType = typeof(ITestProperty);
            Type expectedType = typeof(TestProperty);
            Type overwrittenType = typeof(TestPropertyNoDefaultConstructor);

            _container.Register(new TestPropertyNoDefaultConstructor(1), expectedName)
                .As(registeredType);

            var overwrittenInstance = _container.Resolve(registeredType, expectedName);

            Assert.IsNotNull(overwrittenInstance);

            Assert.IsInstanceOfType(overwrittenInstance, overwrittenType);

            _container.Register(expectedType, expectedName)
                .As(registeredType);

            var actualInstance = _container.Resolve(registeredType, expectedName);

            Assert.IsNotNull(actualInstance);

            Assert.IsInstanceOfType(actualInstance, expectedType);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void register_named_type_null_service_type()
        {
            var expectedName = "test";
            Type expectedType = null;

            // ReSharper disable ExpressionIsAlwaysNull
            _container.Register(expectedType, expectedName);
            // ReSharper restore ExpressionIsAlwaysNull
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void register_named_type_null_name()
        {
            var expectedName = (string)null;
            Type expectedType = typeof(TestProperty);

            // ReSharper disable ExpressionIsAlwaysNull
            _container.Register(expectedType, expectedName);
            // ReSharper restore ExpressionIsAlwaysNull
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void register_named_type_null_name_generic()
        {
            var expectedName = (string)null;

            // ReSharper disable ExpressionIsAlwaysNull
            _container.Register<TestProperty>(expectedName);
            // ReSharper restore ExpressionIsAlwaysNull
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void register_type_for_incompatible_interface()
        {
            var expectedType = typeof(TestService);

            _container.Register(expectedType)
                .As<ITestProperty>();
        }

        [TestMethod]
        public void verify_named_registered_type()
        {
            var expectedName = "test";
            var expectedType = typeof(TestService);
            var expectedRegCount = 1;

            _container.Register(expectedType, expectedName);

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

            var registration = _container.Register(expectedType, expectedName)
                .As(expectedType);

            var actualRegCount = registration.Registrations.Count();

            Assert.AreEqual(expectedRegCount, actualRegCount);

            var actualType = registration.Registrations[expectedType].ServiceType;

            Assert.AreEqual(expectedType, actualType);

            var actualName = registration.Registrations[expectedType].Name;

            Assert.AreEqual(expectedName, actualName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void register_type_inject_null_property_type()
        {
            var expectedType = typeof(ITestService);
            var expectedPropType = (Type)null;

            _container.Register(expectedType)
// ReSharper disable ExpressionIsAlwaysNull
                .Property(expectedPropType);
// ReSharper restore ExpressionIsAlwaysNull

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void register_type_inject_null_property_name()
        {
            var expectedType = typeof(ITestService);
            var expectedPropName = (string) null;

            _container.Register(expectedType)
// ReSharper disable ExpressionIsAlwaysNull
                .Property(expectedPropName);
// ReSharper restore ExpressionIsAlwaysNull

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void register_type_for_non_interface_type()
        {
            var expectedType = typeof(TestService);
            var invalidType = typeof(TestService);

            _container.Register(expectedType)
                .As(invalidType);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void register_type_for_non_interface_type_using_generic()
        {
            var expectedType = typeof(TestService);

            _container.Register(expectedType)
                .As<TestService>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void register_type_for_null_interface_type()
        {
            var expectedType = typeof(TestService);
            var nullType = (Type) null;

            _container.Register(expectedType)
// ReSharper disable ExpressionIsAlwaysNull
                .As(nullType);
// ReSharper restore ExpressionIsAlwaysNull
        }
    }
}
