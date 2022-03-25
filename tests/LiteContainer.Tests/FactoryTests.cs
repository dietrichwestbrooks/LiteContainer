namespace LiteContainer
{
    using System;
    using Xunit;

    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    public class FactoryTests : IDisposable
    {
        private readonly ILiteContainer _container;

        public FactoryTests()
        {
            _container = new LiteContainer();
        }
        
        // Use Dispose to run code after each test has run
        public void Dispose()
        {
            _container.Dispose();
        }

        [Fact]
        public void create_with_no_parameters()
        {
            _container.Register(new Func<ITestService>(() => new TestService()));

            var service = _container.Resolve<ITestService>();

            Assert.NotNull(service);
        }

        [Fact]
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

            Assert.Equal(expectedParamValue1, actualParamValue1);

            Assert.Equal(expectedParamValue2, actualParamValue2);

            Assert.NotNull(service);
        }

        [Fact]
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

            Assert.Equal(expectedNamedValue, actualNamedValue);

            Assert.NotNull(service);
        }

        [Fact]
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

            Assert.Equal(expectedTypedValue, actualTypedValue);

            Assert.NotNull(service);
        }

        [Fact]
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

            Assert.Equal(expectedNamedValue, actualNamedValue);

            Assert.Equal(expectedTypedValue, actualTypedValue);

            Assert.NotNull(service);
        }

        [Fact]
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

            Assert.Equal(expectedParamValue, actualParamValue);

            Assert.NotNull(service);
        }
    }
}
