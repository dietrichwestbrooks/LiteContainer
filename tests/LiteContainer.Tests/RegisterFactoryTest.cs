

// ReSharper disable ConvertToConstant.Local
namespace LiteContainer
{
    using System;
    using CommonServiceLocator;
    using Xunit;

    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    public class RegisterFactoryTest : IDisposable
    {
        private ILiteContainer _container;

        public RegisterFactoryTest()
        {
            _container = new LiteContainer();
        }
        
        // Use Dispose to run code after each test has run
        public void Dispose()
        {
            _container.Dispose();
        }

        [Fact]
        public void create_instance_with_no_parameters()
        {
            var expectedType = typeof(ITestService);

            _container.Register(new Func<ITestService>(() => new TestService()));

            var actualInstance = _container.Resolve<ITestService>();

            Assert.NotNull(actualInstance);

            Assert.IsType<ITestService>(actualInstance);
        }

        [Fact]
        public void create_named_instance_with_no_parameters()
        {
            var expectedName = "test";
            var expectedType = typeof(ITestService);

            var factory = new Func<ITestService>(() => new TestService());

            _container.Register(factory, expectedName);

            var actualInstance = _container.Resolve<ITestService>(expectedName);

            Assert.NotNull(actualInstance);

            Assert.IsType<ITestService>(actualInstance);
        }

        [Fact]
        public void register_null_factory()
        {
            var factory = (Func<ITestService>)null;

            // ReSharper disable ExpressionIsAlwaysNull
        Assert.Throws<ArgumentNullException>(() =>
            _container.Register(factory));
            // ReSharper restore ExpressionIsAlwaysNull
        }

        [Fact]
        public void register_named_null_factory()
        {
            var expectedName = "test";
            var factory = (Func<ITestService>)null;

            // ReSharper disable ExpressionIsAlwaysNull
        Assert.Throws<ArgumentNullException>(() =>
            _container.Register(factory, expectedName));
            // ReSharper restore ExpressionIsAlwaysNull
        }

        [Fact]
        public void register_named_factory_with_null_name()
        {
            var expectedName = (string)null;
            var factory = new Func<ITestService>(() => new TestService());

            // ReSharper disable ExpressionIsAlwaysNull
        Assert.Throws<ArgumentNullException>(() =>
            _container.Register(factory, expectedName));
            // ReSharper restore ExpressionIsAlwaysNull
        }

        [Fact]
        public void register_null_factory_with_object_parameters()
        {
            var factory = (Func<object[], ITestService>)null;

            // ReSharper disable ExpressionIsAlwaysNull
            Assert.Throws<ArgumentNullException>(() =>
            _container.Register(factory));
            // ReSharper restore ExpressionIsAlwaysNull
        }

        [Fact]
        public void register_named_null_factory_with_object_parameters()
        {
            var expectedName = "test";
            var factory = (Func<object[], ITestService>)null;

            // ReSharper disable ExpressionIsAlwaysNull
            Assert.Throws<ArgumentNullException>(() =>
            _container.Register(factory, expectedName));
            // ReSharper restore ExpressionIsAlwaysNull
        }

        [Fact]
        public void register_named_factory_with_object_parameters_and_null_name()
        {
            var expectedName = (string) null;
            var factory = new Func<object[], ITestService>(p => new TestService());

            // ReSharper disable ExpressionIsAlwaysNull
            Assert.Throws<ArgumentNullException>(() =>
            _container.Register(factory, expectedName));
            // ReSharper restore ExpressionIsAlwaysNull
        }

        [Fact]
        public void register_null_factory_with_resolve_parameters()
        {
            var factory = (Func<ResolveParameters, ITestService>) null;

            // ReSharper disable ExpressionIsAlwaysNull
            Assert.Throws<ArgumentNullException>(() =>
            _container.Register(factory));
            // ReSharper restore ExpressionIsAlwaysNull
        }

        [Fact]
        public void register_named_null_factory_with_resolve_parameters()
        {
            var expectedName = "test";
            var factory = (Func<ResolveParameters, ITestService>) null;

            // ReSharper disable ExpressionIsAlwaysNull
        Assert.Throws<ArgumentNullException>(() =>
            _container.Register(factory, expectedName));
            // ReSharper restore ExpressionIsAlwaysNull
        }

        [Fact]
        public void register_named_factory_with_resolve_parameters_and_null_name()
        {
            var expectedName = (string) null;
            var factory = new Func<ResolveParameters, ITestService>(p => new TestService());

            // ReSharper disable ExpressionIsAlwaysNull
        Assert.Throws<ArgumentNullException>(() =>
            _container.Register(factory, expectedName));
            // ReSharper restore ExpressionIsAlwaysNull
        }

        [Fact]
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

            Assert.NotNull(actualInstance);

            Assert.IsType<ITestService>(actualInstance);

            Assert.Equal(expectedParamValue1, actualParamValue1);

            Assert.Equal(expectedParamValue2, actualParamValue2);
        }

        [Fact]
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

            Assert.NotNull(actualInstance);

            Assert.IsType<ITestService>(actualInstance);

            Assert.Equal(expectedParamValue1, actualParamValue1);

            Assert.Equal(expectedParamValue2, actualParamValue2);
        }

        [Fact]
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

            Assert.NotNull(actualInstance);

            Assert.IsType<ITestService>(actualInstance);

            Assert.Equal(expectedNamedValue, actualNamedValue);
        }

        [Fact]
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

            Assert.NotNull(actualInstance);

            Assert.IsType<ITestService>(actualInstance);

            Assert.Equal(expectedNamedValue, actualNamedValue);
        }

        [Fact]
        public void create_instance_with_missing_named_parameter()
        {
            _container.Register(new Func<ResolveParameters, ITestService>(p =>
            {
                p.NamedParameter<int>("named");
                return new TestService();
            }));

        Assert.Throws<ActivationException>(() =>
            _container.Resolve<ITestService>(new NamedParameter<string>("notused", string.Empty)));
        }

        [Fact]
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

            Assert.NotNull(actualInstance);

            Assert.IsType<ITestService>(actualInstance);

            Assert.Equal(expectedTypedValue, actualTypedValue);
        }

        [Fact]
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

            Assert.NotNull(actualInstance);

            Assert.IsType<ITestService>(actualInstance);

            Assert.Equal(expectedTypedValue, actualTypedValue);
        }

        [Fact]
        public void create_instance_with_missing_typed_parameter()
        {
            _container.Register(new Func<ResolveParameters, ITestService>(p =>
            {
                p.TypedParameter<int>();
                return new TestService();
            }));

        Assert.Throws<ActivationException>(() =>
            _container.Resolve<ITestService>(new TypedParameter<string>("notused")));
        }

        [Fact]
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

            Assert.NotNull(actualInstance);

            Assert.IsType<ITestService>(actualInstance);

            Assert.Equal(expectedNamedValue, actualNamedValue);

            Assert.Equal(expectedTypedValue, actualTypedValue);
        }

        [Fact]
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

            Assert.NotNull(actualInstance);

            Assert.IsType<ITestService>(actualInstance);

            Assert.Equal(expectedNamedValue, actualNamedValue);

            Assert.Equal(expectedTypedValue, actualTypedValue);
        }

        [Fact]
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

            Assert.NotNull(actualInstance);

            Assert.IsType<ITestService>(actualInstance);

            int actualParamValue = actualInstance.Parameter.Value;

            Assert.Equal(expectedParamValue, actualParamValue);
        }

        [Fact]
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

            Assert.NotNull(actualInstance);

            Assert.IsType<ITestService>(actualInstance);

            int actualParamValue = actualInstance.Parameter.Value;

            Assert.Equal(expectedParamValue, actualParamValue);
        }

        [Fact]
        public void register_factory_for_incompatible_interface()
        {
            var factory = new Func<ITestService>(() => new TestService());

        Assert.Throws<ArgumentException>(() =>
            _container.Register(factory)
                .As<ITestProperty>());
        }

        [Fact]
        public void register_factory_with_object_parameters_for_incompatible_interface()
        {
            var factory = new Func<object[], ITestService>(p => new TestService());

        Assert.Throws<ArgumentException>(() =>
            _container.Register(factory)
                .As<ITestProperty>());
        }

        [Fact]
        public void register_factory_with_resolved_parameters_for_incompatible_interface()
        {
            var factory = new Func<ResolveParameters, ITestService>(p => new TestService());

        Assert.Throws<ArgumentException>(() =>
            _container.Register(factory)
                .As<ITestProperty>());
        }
    }
}
