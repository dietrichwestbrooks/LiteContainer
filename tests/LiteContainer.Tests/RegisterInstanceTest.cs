

// ReSharper disable ConvertToConstant.Local
namespace LiteContainer
{
    using System;
    using System.Linq;
    using CommonServiceLocator;
    using Xunit;

    /// <summary>
    /// Summary description for InstanceTest
    /// </summary>
    public class RegisterInstanceTest : IDisposable
    {
        private readonly ILiteContainer _container;

        public RegisterInstanceTest()
        {
            _container = new LiteContainer();
        }

        // Use Dispose to run code after each test has run
        public void Dispose()
        {
            _container.Dispose();
        }


        [Fact]
        public void verify_unnamed_registered_instance()
        {
            var expectedType = typeof(TestService);
            var expectedRegCount = 1;

            var expectedInstance = new TestService();

            _container.Register(expectedInstance);

            var actualRegCount = _container.Registrations.Count();

            Assert.Equal(expectedRegCount, actualRegCount);

            var actualType = _container.Registrations.First(r => r.ServiceType == expectedType).ServiceType;

            Assert.Equal(expectedType, actualType);
        }

        [Fact]
        public void verify_unnamed_registered_interface()
        {
            var expectedType = typeof(ITestService);
            var expectedRegCount = 1;

            var expectedInstance = new TestService();

            var registration = _container.Register(expectedInstance)
                .As(expectedType);

            var actualRegCount = registration.Registrations.Count();

            Assert.Equal(expectedRegCount, actualRegCount);

            var actualType = registration.Registrations[expectedType].ServiceType;

            Assert.Equal(expectedType, actualType);
        }

        [Fact]
        public void resolve_unnamed_instance()
        {
            var expectedParamValue = 100;

            var expectedInstance = new TestParameter(expectedParamValue);

            _container.Register(expectedInstance);

            var actualInstance = _container.Resolve<TestParameter>();

            Assert.Same(expectedInstance, actualInstance);

            var actualParamValue = actualInstance.Value;

            Assert.Equal(expectedParamValue, actualParamValue);
        }

        [Fact]
        public void resolve_unnamed_instance_for_interface()
        {
            var expectedParamValue = 100;

            var expectedInstance = new TestParameter(expectedParamValue);

            _container.Register(expectedInstance)
                .As<ITestParameter>();

            var actualInstance = _container.Resolve<ITestParameter>();

            Assert.Same(expectedInstance, actualInstance);

            var actualParamValue = actualInstance.Value;

            Assert.Equal(expectedParamValue, actualParamValue);
        }

        [Fact]
        public void resolve_unnamed_instance_for_multiple_interfaces()
        {
            var expectedParamValue = 100;
            var expectedParamName = "Param1";

            var expectedInstance = new TestParameter(expectedParamValue) { Name = expectedParamName };

            _container.Register(expectedInstance)
                .As<ITestParameterValue>()
                .As<ITestParameterName>();

            var actualInstance = _container.Resolve<ITestParameterValue>();

            Assert.Same(expectedInstance, actualInstance);

            var actualParamValue = actualInstance.Value;

            Assert.Equal(expectedParamValue, actualParamValue);

            var actualInstance2 = _container.Resolve<ITestParameterName>();

            Assert.Same(expectedInstance, actualInstance2);

            var actualParamName = actualInstance2.Name;

            Assert.Equal(expectedParamName, actualParamName);
        }

        [Fact]
        public void resolve_unnamed_instance_for_unregistered_interface()
        {
            var expectedInstance = new TestParameter();

            _container.Register(expectedInstance);

        Assert.Throws<ActivationException>(() =>
            _container.Resolve<ITestParameter>());
        }

        [Fact]
        public void resolve_unnamed_instance_overwrite_registered_type()
        {
            Type registeredType = typeof(ITestProperty);
            Type overwrittenType = typeof(TestPropertyNoDefaultConstructor);

            var expectedInstance = new TestProperty();

            _container.Register(overwrittenType)
                .Parameters(typeof(int))
                .As(registeredType);

            var overwrittenInstance = _container.Resolve(registeredType, new NamedParameter<int>("value", 1));

            Assert.NotNull(overwrittenInstance);

            Assert.IsType<TestPropertyNoDefaultConstructor>(overwrittenInstance);

            _container.Register(expectedInstance)
                .As(registeredType);

            var actualInstance = _container.Resolve(registeredType);

            Assert.NotNull(actualInstance);

            Assert.Same(expectedInstance, actualInstance);
        }

        [Fact]
        public void resolve_unnamed_instance_overwrite_registered_instance()
        {
            Type registeredType = typeof(ITestProperty);
            Type overwrittenType = typeof(TestPropertyNoDefaultConstructor);

            var expectedInstance = new TestProperty();

            _container.Register(new TestPropertyNoDefaultConstructor(1))
                .As(registeredType);

            var overwrittenInstance = _container.Resolve(registeredType);

            Assert.NotNull(overwrittenInstance);

            Assert.IsType<TestPropertyNoDefaultConstructor>(overwrittenInstance);

            _container.Register(expectedInstance)
                .As(registeredType);

            var actualInstance = _container.Resolve(registeredType);

            Assert.NotNull(actualInstance);

            Assert.Same(expectedInstance, actualInstance);
        }

        [Fact]
        public void resolve_named_instance_when_incorrect_name()
        {
            var expectedName = "test";
            var badName = "badName";

            var expectedInstance = new TestParameter();

            _container.Register(expectedInstance, expectedName);

            Assert.Throws<ActivationException>(() =>
            _container.Resolve<TestParameter>(badName));
        }

        [Fact]
        public void register_unnamed_instance_null_service()
        {
            var expectedInstance = (TestProperty)null;

            // ReSharper disable ExpressionIsAlwaysNull
        Assert.Throws<ArgumentNullException>(() =>
            _container.Register(expectedInstance));
            // ReSharper restore ExpressionIsAlwaysNull
        }

        [Fact]
        public void resolve_named_instance()
        {
            var expectedName = "test";
            var expectedParamValue = 100;

            var expectedInstance = new TestParameter(expectedParamValue);

            _container.Register(expectedInstance, expectedName);

            var actualInstance = _container.Resolve<TestParameter>(expectedName);

            Assert.Same(expectedInstance, actualInstance);

            var actualParamValue = actualInstance.Value;

            Assert.Equal(expectedParamValue, actualParamValue);
        }

        [Fact]
        public void resolve_named_instance_for_interface()
        {
            var expectedName = "test";
            var expectedParamValue = 100;

            var expectedInstance = new TestParameter(expectedParamValue);

            _container.Register(expectedInstance, expectedName)
                .As<ITestParameter>();

            var actualInstance = _container.Resolve<ITestParameter>(expectedName);

            Assert.Same(expectedInstance, actualInstance);

            var actualParamValue = actualInstance.Value;

            Assert.Equal(expectedParamValue, actualParamValue);
        }

        [Fact]
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

            Assert.Same(expectedInstance, actualInstance);

            var actualParamValue = actualInstance.Value;

            Assert.Equal(expectedParamValue, actualParamValue);

            var resolvedInstance2 = _container.Resolve<ITestParameterName>(expectedName);

            Assert.True(expectedInstance == resolvedInstance2);

            var actualParamName = resolvedInstance2.Name;

            Assert.Equal(expectedParamName, actualParamName);
        }

        [Fact]
        public void resolve_named_instance_for_unregistered_interface()
        {
            var expectedName = "test";

            var expectedInstance = new TestParameter();

            _container.Register(expectedInstance, expectedName);

        Assert.Throws<ActivationException>(() =>
            _container.Resolve<ITestParameter>());
        }

        [Fact]
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

            Assert.NotNull(overwrittenInstance);

            Assert.IsType<TestPropertyNoDefaultConstructor>(overwrittenInstance);

            _container.Register(expectedInstance, expectedName)
                .As(registeredType);

            var actualInstance = _container.Resolve(registeredType, expectedName);

            Assert.NotNull(actualInstance);

            Assert.Same(expectedInstance, actualInstance);
        }

        [Fact]
        public void resolve_named_instance_overwrite_registered_instance()
        {
            var expectedName = "test";
            Type registeredType = typeof(ITestProperty);
            Type overwrittenType = typeof(TestPropertyNoDefaultConstructor);

            var expectedInstance = new TestProperty();

            _container.Register(new TestPropertyNoDefaultConstructor(1), expectedName)
                .As(registeredType);

            var overwrittenInstance = _container.Resolve(registeredType, expectedName);

            Assert.NotNull(overwrittenInstance);

            Assert.IsType<TestPropertyNoDefaultConstructor>(overwrittenInstance);

            _container.Register(expectedInstance, expectedName)
                .As(registeredType);

            var actualInstance = _container.Resolve(registeredType, expectedName);

            Assert.NotNull(actualInstance);

            Assert.Same(expectedInstance, actualInstance);
        }

        [Fact]
        public void register_named_instance_null_name()
        {
            var expectedName = (string)null;
            var expectedInstance = new TestProperty();

            // ReSharper disable ExpressionIsAlwaysNull
        Assert.Throws<ArgumentNullException>(() =>
            _container.Register(expectedInstance, expectedName));
            // ReSharper restore ExpressionIsAlwaysNull
        }

        [Fact]
        public void register_named_instance_null_service()
        {
            var expectedName = "test";
            var expectedInstance = (TestProperty)null;

            // ReSharper disable ExpressionIsAlwaysNull
        Assert.Throws<ArgumentNullException>(() =>
            _container.Register(expectedInstance, expectedName));
            // ReSharper restore ExpressionIsAlwaysNull
        }

        [Fact]
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

            Assert.Equal(expectedRegCount, _container.Registrations.Count());

            var actualInstances = _container.ResolveAll(expectedType).Cast<TestProperty>().ToArray();

            Assert.NotNull(actualInstances);

            Assert.Equal(expectedResolveCount, actualInstances.Count());

            var actualInstance = actualInstances.FirstOrDefault(p => p.Value == expectedValue);

            Assert.Same(expectedInstance, actualInstance);

            var actualInstance2 = actualInstances.FirstOrDefault(p => p.Value == expectedValue2);

            Assert.Same(expectedInstance2, actualInstance2);

            var actualInstance3 = actualInstances.FirstOrDefault(p => p.Value == expectedValue3);

            Assert.Null(actualInstance3);
        }

        [Fact]
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

            Assert.Equal(expectedRegCount, _container.Registrations.Count());

            var actualInstances = _container.ResolveAll<TestProperty>().ToArray();

            Assert.NotNull(actualInstances);

            Assert.Equal(expectedResolveCount, actualInstances.Count());

            var actualInstance = actualInstances.FirstOrDefault(p => p.Value == expectedValue);

            Assert.Same(expectedInstance, actualInstance);

            var actualInstance2 = actualInstances.FirstOrDefault(p => p.Value == expectedValue2);

            Assert.Same(expectedInstance2, actualInstance2);

            var actualInstance3 = actualInstances.FirstOrDefault(p => p.Value == expectedValue3);

            Assert.Null(actualInstance3);
        }

        [Fact]
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

            Assert.Equal(expectedRegCount, _container.Registrations.Count());

            var actualInstances = _container.ResolveAll(expectedType).Cast<TestProperty>().ToArray();

            Assert.NotNull(actualInstances);

            Assert.Equal(expectedResolveCount, actualInstances.Count());
        }

        [Fact]
        public void resolve_all_null_service_type()
        {
            var expectedType = (Type)null;

// ReSharper disable ExpressionIsAlwaysNull
        Assert.Throws<ArgumentNullException>(() =>
            _container.ResolveAll(expectedType));
// ReSharper restore ExpressionIsAlwaysNull
        }

        [Fact]
        public void register_instance_for_incompatible_interface()
        {
            var expectedInstance = new TestService();

        Assert.Throws<ArgumentException>(() =>
            _container.Register(expectedInstance)
                .As<ITestProperty>());
        }

        [Fact]
        public void verify_named_registered_instance()
        {
            var expectedName = "test";
            var expectedType = typeof(TestService);
            var expectedRegCount = 1;

            var expectedInstance = new TestService();

            _container.Register(expectedInstance, expectedName);

            var actualRegCount = _container.Registrations.Count();

            Assert.Equal(expectedRegCount, actualRegCount);

            var actualType = _container.Registrations.First(r => r.ServiceType == expectedType).ServiceType;

            Assert.Equal(expectedType, actualType);

            var actualName = _container.Registrations.First(r => r.ServiceType == expectedType).Name;

            Assert.Equal(expectedName, actualName);
        }

        [Fact]
        public void verify_named_registered_interface()
        {
            var expectedName = "test";
            var expectedType = typeof(ITestService);
            var expectedRegCount = 1;

            var expectedInstance = new TestService();

            var registration = _container.Register(expectedInstance, expectedName)
                .As(expectedType);

            var actualRegCount = registration.Registrations.Count();

            Assert.Equal(expectedRegCount, actualRegCount);

            var actualType = registration.Registrations[expectedType].ServiceType;

            Assert.Equal(expectedType, actualType);

            var actualName = registration.Registrations[expectedType].Name;

            Assert.Equal(expectedName, actualName);
        }
    }
}
