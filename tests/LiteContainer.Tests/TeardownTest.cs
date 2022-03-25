namespace LiteContainer
{
    using System;
    using Xunit;

    /// <summary>
    /// Summary description for TeardownTest
    /// </summary>
    public class TeardownTest : IDisposable
    {
        private ILiteContainer _container;

        public TeardownTest()
        {
            _container = new LiteContainer();
        }

        // Use Dispose to run code after each test has run
        public void Dispose()
        {
            _container.Dispose();
        }

        [Fact]
        public void teardown_service_instance_that_implements_idisposable()
        {
            var registeredType = typeof(TestService);

            _container.Register(registeredType);

            var registeredInstance = _container.Resolve(registeredType);

            //Verify no exceptions are thrown
            _container.TearDown(registeredInstance);
        }

        [Fact]
        public void teardown_service_instance_that_does_not_implement_idisposable()
        {
            var registeredType = typeof(TestProperty);

            _container.Register(registeredType);

            var registeredInstance = _container.Resolve(registeredType);

            //Verify no exceptions are thrown
            _container.TearDown(registeredInstance);
        }

        [Fact]
        public void teardown_unregister_service_instance()
        {
            var externalInstance = new TestService();

            //Verify no exceptions are thrown
            _container.TearDown(externalInstance);
        }
    }
}
