using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wayne.Payment.Platform.Lite;

namespace LiteContainer.Test
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class FactoryTests
    {
        private ILiteContainer _container;

        public FactoryTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext _testContext;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return _testContext;
            }
            set
            {
                _testContext = value;
            }
        }

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
        public void MyTestInitialize()
        {
            _container = new Wayne.Payment.Platform.Lite.LiteContainer();
        }
        
        // Use TestCleanup to run code after each test has run
        [TestCleanup]
        public void MyTestCleanup()
        {
            _container.Dispose();
        }
        
        #endregion

        [TestMethod]
        public void create_with_no_parameters()
        {
            _container.Register(new Func<ITestService>(() => new TestService()));

            var service = _container.Resolve<ITestService>();

            Assert.IsNotNull(service, "Service is null");
        }

        [TestMethod]
        public void create_with_ordered_parameters()
        {
            int expectedParamValue1 = 1;
            int actualParamValue1 = 0;
            string expectedParamValue2 = "Two";
            string actualParamValue2 = string.Empty;

            _container.Register(new Func<object[], ITestService>(p =>
            {
                if (p.Length >= 1)
                    actualParamValue1 = (int)p[0];

                if (p.Length == 2)
                    actualParamValue2 = (string)p[1];

                return new TestService();
            }));

            var service = _container.Resolve<ITestService>(new OrderedParameters(expectedParamValue1, expectedParamValue2));

            Assert.AreEqual(expectedParamValue1, actualParamValue1);

            Assert.AreEqual(expectedParamValue2, actualParamValue2);

            Assert.IsNotNull(service, "Service is null");
        }

        [TestMethod]
        public void create_with_named_parameter()
        {
            int expectedNamedValue = 1;
            int actualNamedValue = 0;

            _container.Register(new Func<ResolveParameters, ITestService>(p =>
            {
                actualNamedValue = p.NamedParameter<int>("named");
                return new TestService();
            }));

            var service = _container.Resolve<ITestService>(new NamedParameter<int>("named", expectedNamedValue));

            Assert.AreEqual(expectedNamedValue, actualNamedValue);

            Assert.IsNotNull(service, "Service is null");
        }

        [TestMethod]
        public void create_with_typed_value_parameter()
        {
            int expectedTypedValue = 1;
            int actualTypedValue = 0;

            _container.Register(new Func<ResolveParameters, ITestService>(p =>
            {
                actualTypedValue = p.TypedParameter<int>();
                return new TestService();
            }));

            var service = _container.Resolve<ITestService>(new TypedParameter<int>(expectedTypedValue));

            Assert.AreEqual(expectedTypedValue, actualTypedValue);

            Assert.IsNotNull(service, "Service is null");
        }

        [TestMethod]
        public void create_with_named_and_typed_parameter()
        {
            int expectedNamedValue = 1;
            int actualNamedValue = 0;
            int expectedTypedValue = 1;
            int actualTypedValue = 0;

            _container.Register(new Func<ResolveParameters, ITestService>(p =>
            {
                actualNamedValue = p.NamedParameter<int>("named");
                actualTypedValue = p.TypedParameter<int>();
                return new TestService();
            }));

            var service = _container.Resolve<ITestService>(new NamedParameter<int>("named", expectedNamedValue),
                new TypedParameter<int>(1));

            Assert.AreEqual(expectedNamedValue, actualNamedValue);

            Assert.AreEqual(expectedTypedValue, actualTypedValue);

            Assert.IsNotNull(service, "Service is null");
        }

        [TestMethod]
        public void create_with_typed_obj_parameter()
        {
            int expectedParamValue = 1;
            int actualParamValue;

            _container.Register(new Func<ResolveParameters, ITestService>(p =>
            {
                var parameter = p.TypedParameter<ITestParameter>();
                return new TestService(parameter);
            }));

            var service = _container.Resolve<ITestService>(new TypedParameter<ITestParameter>(new TestParameter(expectedParamValue)));

            actualParamValue = service.Parameter.Value;

            Assert.AreEqual(expectedParamValue, actualParamValue);

            Assert.IsNotNull(service, "Service is null");
        }
    }
}
