

// ReSharper disable ConvertToConstant.Local
namespace LiteContainer
{
    using System;
    using Xunit;

    /// <summary>
    /// Summary description for BuildUpTest
    /// </summary>
    public class BuildUpTest : IDisposable
    {
        private readonly ILiteContainer _container;

        public BuildUpTest()
        {
            _container = new LiteContainer();
        }

        // Use Dispose to run code after each test has run
        public void Dispose()
        {
            _container.Dispose();
        }

        [Fact]
        public void buildup_unregistered_unnamed_service_type()
        {
            var unregisteredType = typeof(ITestService);

            var externalInstance = new TestService();

            _container.BuildUp(externalInstance);

            Assert.Null(externalInstance.Parameter);
        }

        [Fact]
        public void buildup_unnamed_service_type_with_unassignable_interface()
        {
            var unasignableType = typeof(ITestProperty);

            var externalInstance = new TestService();

        Assert.Throws<ArgumentException>(() =>
            _container.BuildUp(externalInstance));
        }

        [Fact]
        public void buildup_null_unnamed_service_type()
        {
            var nullServiceType = (Type)null;

            var externalInstance = new TestService();

// ReSharper disable ExpressionIsAlwaysNull
        Assert.Throws<ArgumentNullException>(() =>
            _container.BuildUp(externalInstance));
// ReSharper restore ExpressionIsAlwaysNull
        }

        [Fact]
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

            _container.BuildUp(externalInstance);

            var actualParamValue = externalInstance.Parameter.Value;

            Assert.Equal(expectedParamValue, actualParamValue);
        }

        [Fact]
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

            _container.BuildUp(externalInstance);

            var actualParamValue = externalInstance.Parameter.Value;

            Assert.Equal(expectedParamValue, actualParamValue);
        }

        [Fact]
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

            _container.BuildUp(externalInstance);

            var actualParamValue = externalInstance.Parameter.Value;

            Assert.Equal(expectedParamValue, actualParamValue);
        }

        [Fact]
        public void buildup_null_named_service_type()
        {
            var expectedName = (string) null;
            var registeredType = typeof(ITestService);
            var propName = "Parameter";

            _container.Register(registeredType)
                .Property(propName);

            var externalInstance = new TestService();

// ReSharper disable ExpressionIsAlwaysNull
        Assert.Throws<ArgumentNullException>(() =>
            _container.BuildUp(externalInstance, expectedName));
// ReSharper restore ExpressionIsAlwaysNull

            Assert.Null(externalInstance.Parameter);
        }

        [Fact]
        public void buildup_null_named_service_type_using_generic()
        {
            var expectedName = (string)null;
            var registeredType = typeof(ITestService);
            var propName = "Parameter";

            _container.Register(registeredType)
                .Property(propName);

            var externalInstance = new TestService();

            // ReSharper disable ExpressionIsAlwaysNull
        Assert.Throws<ArgumentNullException>(() =>
            _container.BuildUp(externalInstance, expectedName));
            // ReSharper restore ExpressionIsAlwaysNull

            Assert.Null(externalInstance.Parameter);
        }

        [Fact]
        public void buildup_unregistered_named_service_type()
        {
            var expectedName = "test";
            var unregisteredType = typeof(ITestService);

            var externalInstance = new TestService();

            _container.BuildUp(externalInstance, expectedName);

            Assert.Null(externalInstance.Parameter);
        }

        [Fact]
        public void buildup_named_service_type_with_unassignable_interface()
        {
            var expectedName = "test";
            var unasignableType = typeof(ITestProperty);

            var externalInstance = new TestService();

        Assert.Throws<ArgumentException>(() =>
            _container.BuildUp(externalInstance, expectedName));
        }

        [Fact]
        public void buildup_named_null_service_type()
        {
            var expectedName = "test";
            var nullServiceType = (Type)null;

            var externalInstance = new TestService();

// ReSharper disable ExpressionIsAlwaysNull
        Assert.Throws<ArgumentNullException>(() =>
            _container.BuildUp(externalInstance, expectedName));
// ReSharper restore ExpressionIsAlwaysNull
        }

        [Fact]
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

            _container.BuildUp(externalInstance, expectedName);

            var actualParamValue = externalInstance.Parameter.Value;

            Assert.Equal(expectedParamValue, actualParamValue);
        }

        [Fact]
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

            _container.BuildUp(externalInstance, expectedName);

            var actualParamValue = externalInstance.Parameter.Value;

            Assert.Equal(expectedParamValue, actualParamValue);
        }

        [Fact]
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

            _container.BuildUp(externalInstance, expectedName);

            var actualParamValue = externalInstance.Parameter.Value;

            Assert.Equal(expectedParamValue, actualParamValue);
        }
    }
}
