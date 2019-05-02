using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wayne.Payment.Platform.Lite;

namespace LiteContainer.Test
{
    /// <summary>
    /// Summary description for TeardownTest
    /// </summary>
    [TestClass]
    public class TeardownTest
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
        public void teardown_service_instance_that_implements_idisposable()
        {
            var registeredType = typeof(TestService);

            _container.Register(registeredType);

            var registeredInstance = _container.Resolve(registeredType);

            //Verify no exceptions are thrown
            _container.TearDown(registeredInstance);
        }

        [TestMethod]
        public void teardown_service_instance_that_does_not_implement_idisposable()
        {
            var registeredType = typeof(TestProperty);

            _container.Register(registeredType);

            var registeredInstance = _container.Resolve(registeredType);

            //Verify no exceptions are thrown
            _container.TearDown(registeredInstance);
        }

        [TestMethod]
        public void teardown_unregister_service_instance()
        {
            var externalInstance = new TestService();

            //Verify no exceptions are thrown
            _container.TearDown(externalInstance);
        }
    }
}
