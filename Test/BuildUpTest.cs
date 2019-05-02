using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wayne.Payment.Platform.Lite;

// ReSharper disable ConvertToConstant.Local
namespace LiteContainer.Test
{
    /// <summary>
    /// Summary description for BuildUpTest
    /// </summary>
    [TestClass]
    public class BuildUpTest
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
        public void buildup_unregistered_unnamed_service_type()
        {
            var unregisteredType = typeof(ITestService);

            var externalInstance = new TestService();

            _container.BuildUp(unregisteredType, externalInstance);

            Assert.IsNull(externalInstance.Parameter);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void buildup_unnamed_service_type_with_unassignable_interface()
        {
            var unasignableType = typeof(ITestProperty);

            var externalInstance = new TestService();

            _container.BuildUp(unasignableType, externalInstance);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void buildup_null_unnamed_service_type()
        {
            var nullServiceType = (Type)null;

            var externalInstance = new TestService();

// ReSharper disable ExpressionIsAlwaysNull
            _container.BuildUp(nullServiceType, externalInstance);
// ReSharper restore ExpressionIsAlwaysNull
        }

        [TestMethod]
        public void inject_property_by_name_for_externally_created_instance_for_unnamed_service_type()
        {
            var expectedType = typeof(ITestService);
            var propType = typeof(ITestParameter);
            var propName = "Parameter";
            var expectedParamValue = 100;

            var propInstance = new TestParameter(expectedParamValue);

            _container.Register(propInstance)
                .As(propType);

            _container.Register(expectedType)
                .Property(propName);

            var externalInstance = new TestService();

            _container.BuildUp(expectedType, externalInstance);

            var actualParamValue = externalInstance.Parameter.Value;

            Assert.AreEqual(expectedParamValue, actualParamValue);
        }

        [TestMethod]
        public void inject_property_by_name_for_externally_created_instance_for_unnamed_service_type_using_generic()
        {
            var expectedType = typeof(ITestService);
            var propType = typeof(ITestParameter);
            var propName = "Parameter";
            var expectedParamValue = 100;

            var propInstance = new TestParameter(expectedParamValue);

            _container.Register(propInstance)
                .As(propType);

            _container.Register(expectedType)
                .Property(propName);

            var externalInstance = new TestService();

            _container.BuildUp<ITestService>(externalInstance);

            var actualParamValue = externalInstance.Parameter.Value;

            Assert.AreEqual(expectedParamValue, actualParamValue);
        }

        [TestMethod]
        public void inject_property_by_type_for_externally_created_instance_for_unnamed_service_type()
        {
            var expectedType = typeof(ITestService);
            var propType = typeof(ITestParameter);
            var expectedParamValue = 100;

            var propInstance = new TestParameter(expectedParamValue);

            _container.Register(propInstance)
                .As(propType);

            _container.Register(expectedType)
                .Property(propType);

            var externalInstance = new TestService();

            _container.BuildUp(expectedType, externalInstance);

            var actualParamValue = externalInstance.Parameter.Value;

            Assert.AreEqual(expectedParamValue, actualParamValue);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void buildup_null_named_service_type()
        {
            var expectedName = (string) null;
            var registeredType = typeof(ITestService);
            var propName = "Parameter";

            _container.Register(registeredType)
                .Property(propName);

            var externalInstance = new TestService();

// ReSharper disable ExpressionIsAlwaysNull
            _container.BuildUp(registeredType, externalInstance, expectedName);
// ReSharper restore ExpressionIsAlwaysNull

            Assert.IsNull(externalInstance.Parameter);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void buildup_null_named_service_type_using_generic()
        {
            var expectedName = (string)null;
            var registeredType = typeof(ITestService);
            var propName = "Parameter";

            _container.Register(registeredType)
                .Property(propName);

            var externalInstance = new TestService();

            // ReSharper disable ExpressionIsAlwaysNull
            _container.BuildUp<ITestService>(externalInstance, expectedName);
            // ReSharper restore ExpressionIsAlwaysNull

            Assert.IsNull(externalInstance.Parameter);
        }

        [TestMethod]
        public void buildup_unregistered_named_service_type()
        {
            var expectedName = "test";
            var unregisteredType = typeof(ITestService);

            var externalInstance = new TestService();

            _container.BuildUp(unregisteredType, externalInstance, expectedName);

            Assert.IsNull(externalInstance.Parameter);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void buildup_named_service_type_with_unassignable_interface()
        {
            var expectedName = "test";
            var unasignableType = typeof(ITestProperty);

            var externalInstance = new TestService();

            _container.BuildUp(unasignableType, externalInstance, expectedName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void buildup_named_null_service_type()
        {
            var expectedName = "test";
            var nullServiceType = (Type)null;

            var externalInstance = new TestService();

// ReSharper disable ExpressionIsAlwaysNull
            _container.BuildUp(nullServiceType, externalInstance, expectedName);
// ReSharper restore ExpressionIsAlwaysNull
        }

        [TestMethod]
        public void inject_property_by_name_for_externally_created_instance_for_named_service_type()
        {
            var expectedName = "test";
            var expectedType = typeof(ITestService);
            var propType = typeof(ITestParameter);
            var propName = "Parameter";
            var expectedParamValue = 100;

            var propInstance = new TestParameter(expectedParamValue);

            _container.Register(propInstance)
                .As(propType);

            _container.Register(expectedType, expectedName)
                .Property(propName);

            var externalInstance = new TestService();

            _container.BuildUp(expectedType, externalInstance, expectedName);

            var actualParamValue = externalInstance.Parameter.Value;

            Assert.AreEqual(expectedParamValue, actualParamValue);
        }

        [TestMethod]
        public void inject_property_by_name_for_externally_created_instance_for_named_service_type_using_generic()
        {
            var expectedName = "test";
            var expectedType = typeof(ITestService);
            var propType = typeof(ITestParameter);
            var propName = "Parameter";
            var expectedParamValue = 100;

            var propInstance = new TestParameter(expectedParamValue);

            _container.Register(propInstance)
                .As(propType);

            _container.Register(expectedType, expectedName)
                .Property(propName);

            var externalInstance = new TestService();

            _container.BuildUp<ITestService>(externalInstance, expectedName);

            var actualParamValue = externalInstance.Parameter.Value;

            Assert.AreEqual(expectedParamValue, actualParamValue);
        }

        [TestMethod]
        public void inject_property_by_type_for_externally_created_instance_for_named_service_type()
        {
            var expectedName = "test";
            var expectedType = typeof(ITestService);
            var propType = typeof(ITestParameter);
            var expectedParamValue = 100;

            var propInstance = new TestParameter(expectedParamValue);

            _container.Register(propInstance)
                .As(propType);

            _container.Register(expectedType, expectedName)
                .Property(propType);

            var externalInstance = new TestService();

            _container.BuildUp(expectedType, externalInstance, expectedName);

            var actualParamValue = externalInstance.Parameter.Value;

            Assert.AreEqual(expectedParamValue, actualParamValue);
        }
    }
}
