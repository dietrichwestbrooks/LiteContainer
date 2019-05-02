using System;
using Microsoft.Practices.ServiceLocation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wayne.Payment.Platform.Lite;

// ReSharper disable ConvertToConstant.Local
namespace LiteContainer.Test
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class RegisterFactoryTest
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
        public void create_instance_with_no_parameters()
        {
            var expectedType = typeof(ITestService);

            _container.Register(new Func<ITestService>(() => new TestService()));

            var actualInstance = _container.Resolve<ITestService>();

            Assert.IsNotNull(actualInstance);

            Assert.IsInstanceOfType(actualInstance, expectedType);
        }

        [TestMethod]
        public void create_named_instance_with_no_parameters()
        {
            var expectedName = "test";
            var expectedType = typeof(ITestService);

            var factory = new Func<ITestService>(() => new TestService());

            _container.Register(factory, expectedName);

            var actualInstance = _container.Resolve<ITestService>(expectedName);

            Assert.IsNotNull(actualInstance);

            Assert.IsInstanceOfType(actualInstance, expectedType);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void register_null_factory()
        {
            var factory = (Func<ITestService>)null;

            // ReSharper disable ExpressionIsAlwaysNull
            _container.Register(factory);
            // ReSharper restore ExpressionIsAlwaysNull
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void register_named_null_factory()
        {
            var expectedName = "test";
            var factory = (Func<ITestService>)null;

            // ReSharper disable ExpressionIsAlwaysNull
            _container.Register(factory, expectedName);
            // ReSharper restore ExpressionIsAlwaysNull
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void register_named_factory_with_null_name()
        {
            var expectedName = (string)null;
            var factory = new Func<ITestService>(() => new TestService());

            // ReSharper disable ExpressionIsAlwaysNull
            _container.Register(factory, expectedName);
            // ReSharper restore ExpressionIsAlwaysNull
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void register_null_factory_with_object_parameters()
        {
            var factory = (Func<object[], ITestService>)null;

            // ReSharper disable ExpressionIsAlwaysNull
            _container.Register(factory);
            // ReSharper restore ExpressionIsAlwaysNull
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void register_named_null_factory_with_object_parameters()
        {
            var expectedName = "test";
            var factory = (Func<object[], ITestService>)null;

            // ReSharper disable ExpressionIsAlwaysNull
            _container.Register(factory, expectedName);
            // ReSharper restore ExpressionIsAlwaysNull
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void register_named_factory_with_object_parameters_and_null_name()
        {
            var expectedName = (string) null;
            var factory = new Func<object[], ITestService>(p => new TestService());

            // ReSharper disable ExpressionIsAlwaysNull
            _container.Register(factory, expectedName);
            // ReSharper restore ExpressionIsAlwaysNull
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void register_null_factory_with_resolve_parameters()
        {
            var factory = (Func<ResolveParameters, ITestService>) null;

            // ReSharper disable ExpressionIsAlwaysNull
            _container.Register(factory);
            // ReSharper restore ExpressionIsAlwaysNull
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void register_named_null_factory_with_resolve_parameters()
        {
            var expectedName = "test";
            var factory = (Func<ResolveParameters, ITestService>) null;

            // ReSharper disable ExpressionIsAlwaysNull
            _container.Register(factory, expectedName);
            // ReSharper restore ExpressionIsAlwaysNull
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void register_named_factory_with_resolve_parameters_and_null_name()
        {
            var expectedName = (string) null;
            var factory = new Func<ResolveParameters, ITestService>(p => new TestService());

            // ReSharper disable ExpressionIsAlwaysNull
            _container.Register(factory, expectedName);
            // ReSharper restore ExpressionIsAlwaysNull
        }

        [TestMethod]
        public void create_instance_using_ordered_parameters()
        {
            var expectedType = typeof(ITestService);
            var expectedParamValue1 = 1;
            var actualParamValue1 = 0;
            var expectedParamValue2 = "Two";
            var actualParamValue2 = string.Empty;

            _container.Register(new Func<object[], ITestService>(p =>
            {
                if (p.Length >= 1)
                    actualParamValue1 = (int)p[0];

                if (p.Length == 2)
                    actualParamValue2 = (string)p[1];

                return new TestService();
            }));

            var actualInstance = _container.Resolve<ITestService>(new OrderedParameters(expectedParamValue1, expectedParamValue2));

            Assert.IsNotNull(actualInstance);

            Assert.IsInstanceOfType(actualInstance, expectedType);

            Assert.AreEqual(expectedParamValue1, actualParamValue1);

            Assert.AreEqual(expectedParamValue2, actualParamValue2);
        }

        [TestMethod]
        public void create_named_instance_using_ordered_parameters()
        {
            var expectedName = "test";
            var expectedType = typeof(ITestService);
            var expectedParamValue1 = 1;
            var actualParamValue1 = 0;
            var expectedParamValue2 = "Two";
            var actualParamValue2 = string.Empty;

            _container.Register(new Func<object[], ITestService>(p =>
            {
                if (p.Length >= 1)
                    actualParamValue1 = (int)p[0];

                if (p.Length == 2)
                    actualParamValue2 = (string)p[1];

                return new TestService();
            }), expectedName);

            var actualInstance = _container.Resolve<ITestService>(expectedName, 
                new OrderedParameters(expectedParamValue1, expectedParamValue2));

            Assert.IsNotNull(actualInstance);

            Assert.IsInstanceOfType(actualInstance, expectedType);

            Assert.AreEqual(expectedParamValue1, actualParamValue1);

            Assert.AreEqual(expectedParamValue2, actualParamValue2);
        }

        [TestMethod]
        public void create_instance_with_named_parameter()
        {
            var expectedType = typeof(ITestService);
            var expectedNamedValue = 1;
            var actualNamedValue = 0;

            _container.Register(new Func<ResolveParameters, ITestService>(p =>
            {
                actualNamedValue = p.NamedParameter<int>("named");
                return new TestService();
            }));

            var actualInstance = _container.Resolve<ITestService>(new NamedParameter<int>("named", expectedNamedValue));

            Assert.IsNotNull(actualInstance);

            Assert.IsInstanceOfType(actualInstance, expectedType);

            Assert.AreEqual(expectedNamedValue, actualNamedValue);
        }

        [TestMethod]
        public void create_named_instance_with_named_parameter()
        {
            var expectedName = "test";
            var expectedType = typeof(ITestService);
            var expectedNamedValue = 1;
            var actualNamedValue = 0;

            _container.Register(new Func<ResolveParameters, ITestService>(p =>
            {
                actualNamedValue = p.NamedParameter<int>("named");
                return new TestService();
            }), expectedName);

            var actualInstance = _container.Resolve<ITestService>(expectedName, 
                new NamedParameter<int>("named", expectedNamedValue));

            Assert.IsNotNull(actualInstance);

            Assert.IsInstanceOfType(actualInstance, expectedType);

            Assert.AreEqual(expectedNamedValue, actualNamedValue);
        }

        [TestMethod]
        [ExpectedException(typeof(ActivationException))]
        public void create_instance_with_missing_named_parameter()
        {
            _container.Register(new Func<ResolveParameters, ITestService>(p =>
            {
                p.NamedParameter<int>("named");
                return new TestService();
            }));

            _container.Resolve<ITestService>(new NamedParameter<string>("notused", string.Empty));
        }

        [TestMethod]
        public void create_instance_with_typed_parameter()
        {
            var expectedType = typeof(ITestService);
            var expectedTypedValue = 1;
            var actualTypedValue = 0;

            _container.Register(new Func<ResolveParameters, ITestService>(p =>
            {
                actualTypedValue = p.TypedParameter<int>();
                return new TestService();
            }));

            var actualInstance = _container.Resolve<ITestService>(new TypedParameter<int>(expectedTypedValue));

            Assert.IsNotNull(actualInstance);

            Assert.IsInstanceOfType(actualInstance, expectedType);

            Assert.AreEqual(expectedTypedValue, actualTypedValue);
        }

        [TestMethod]
        public void create_named_instance_with_typed_parameter()
        {
            var expectedName = "test";
            var expectedType = typeof(ITestService);
            var expectedTypedValue = 1;
            var actualTypedValue = 0;

            _container.Register(new Func<ResolveParameters, ITestService>(p =>
            {
                actualTypedValue = p.TypedParameter<int>();
                return new TestService();
            }), expectedName);

            var actualInstance = _container.Resolve<ITestService>(expectedName, 
                new TypedParameter<int>(expectedTypedValue));

            Assert.IsNotNull(actualInstance);

            Assert.IsInstanceOfType(actualInstance, expectedType);

            Assert.AreEqual(expectedTypedValue, actualTypedValue);
        }

        [TestMethod]
        [ExpectedException(typeof(ActivationException))]
        public void create_instance_with_missing_typed_parameter()
        {
            _container.Register(new Func<ResolveParameters, ITestService>(p =>
            {
                p.TypedParameter<int>();
                return new TestService();
            }));

            _container.Resolve<ITestService>(new TypedParameter<string>("notused"));
        }

        [TestMethod]
        public void create_instance_with_named_and_typed_parameter()
        {
            var expectedType = typeof(ITestService);
            var expectedNamedValue = 1;
            var actualNamedValue = 0;
            var expectedTypedValue = 1;
            var actualTypedValue = 0;

            _container.Register(new Func<ResolveParameters, ITestService>(p =>
            {
                actualNamedValue = p.NamedParameter<int>("named");
                actualTypedValue = p.TypedParameter<int>();
                return new TestService();
            }));

            var actualInstance = _container.Resolve<ITestService>(new NamedParameter<int>("named", expectedNamedValue),
                new TypedParameter<int>(1));

            Assert.IsNotNull(actualInstance);

            Assert.IsInstanceOfType(actualInstance, expectedType);

            Assert.AreEqual(expectedNamedValue, actualNamedValue);

            Assert.AreEqual(expectedTypedValue, actualTypedValue);
        }

        [TestMethod]
        public void create_named_instance_with_named_and_typed_parameter()
        {
            var expectedName = "test";
            var expectedType = typeof(ITestService);
            var expectedNamedValue = 1;
            var actualNamedValue = 0;
            var expectedTypedValue = 1;
            var actualTypedValue = 0;

            _container.Register(new Func<ResolveParameters, ITestService>(p =>
            {
                actualNamedValue = p.NamedParameter<int>("named");
                actualTypedValue = p.TypedParameter<int>();
                return new TestService();
            }), expectedName);

            var actualInstance = _container.Resolve<ITestService>(expectedName, 
                new NamedParameter<int>("named", expectedNamedValue),
                new TypedParameter<int>(1));

            Assert.IsNotNull(actualInstance);

            Assert.IsInstanceOfType(actualInstance, expectedType);

            Assert.AreEqual(expectedNamedValue, actualNamedValue);

            Assert.AreEqual(expectedTypedValue, actualTypedValue);
        }

        [TestMethod]
        public void create_instance_with_typed_instance_parameter()
        {
            var expectedType = typeof(ITestService);
            var expectedParamValue = 1;

            _container.Register(new Func<ResolveParameters, ITestService>(p =>
            {
                var parameter = p.TypedParameter<ITestParameter>();
                return new TestService(parameter);
            }));

            var actualInstance = _container.Resolve<ITestService>(new TypedParameter<ITestParameter>(new TestParameter(expectedParamValue)));

            Assert.IsNotNull(actualInstance);

            Assert.IsInstanceOfType(actualInstance, expectedType);

            int actualParamValue = actualInstance.Parameter.Value;

            Assert.AreEqual(expectedParamValue, actualParamValue);
        }

        [TestMethod]
        public void create_named_instance_with_typed_instance_parameter()
        {
            var expectedName = "test";
            var expectedType = typeof(ITestService);
            var expectedParamValue = 1;

            _container.Register(new Func<ResolveParameters, ITestService>(p =>
            {
                var parameter = p.TypedParameter<ITestParameter>();
                return new TestService(parameter);
            }), expectedName);

            var actualInstance = _container.Resolve<ITestService>(expectedName,
                new TypedParameter<ITestParameter>(new TestParameter(expectedParamValue)));

            Assert.IsNotNull(actualInstance);

            Assert.IsInstanceOfType(actualInstance, expectedType);

            int actualParamValue = actualInstance.Parameter.Value;

            Assert.AreEqual(expectedParamValue, actualParamValue);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void register_factory_for_incompatible_interface()
        {
            var factory = new Func<ITestService>(() => new TestService());

            _container.Register(factory)
                .As<ITestProperty>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void register_factory_with_object_parameters_for_incompatible_interface()
        {
            var factory = new Func<object[], ITestService>(p => new TestService());

            _container.Register(factory)
                .As<ITestProperty>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void register_factory_with_resolved_parameters_for_incompatible_interface()
        {
            var factory = new Func<ResolveParameters, ITestService>(p => new TestService());

            _container.Register(factory)
                .As<ITestProperty>();
        }
    }
}
