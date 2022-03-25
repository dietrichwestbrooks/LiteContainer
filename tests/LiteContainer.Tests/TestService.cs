namespace LiteContainer
{
    using System;

    public interface ITestService
    {
        int Value { get; }
        ITestParameter Parameter { get; }
    }

    public class TestService : ITestService, IDisposable
    {
        public bool IsDisposed { get; private set; }
        public int Value { get; private set; }
        public ITestParameter Parameter { get; private set; }

        public TestService()
        {

        }

        public TestService(int value)
        {
            Value = value;
        }

        public TestService(ITestParameter parameter)
            : this(0, parameter)
        {
        }

        public TestService(int value, ITestParameter parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException("parameter");

            Value = value;
            Parameter = parameter;
        }

        public ITestProperty TestProperty { get; private set; }

        public ITestProperty TestProperty2 { get; private set; }

        public void Dispose()
        {
            IsDisposed = true;
        }
    }

    public interface ITestParameterValue
    {
        int Value { get; }
    }

    public interface ITestParameterName
    {
        string Name { get; }
    }

    public interface ITestParameter : ITestParameterName, ITestParameterValue
    {
    }

    public class TestParameter : ITestParameter
    {
        public string Name { get; set; }
        public int Value { get; private set; }

        public TestParameter()
            : this("Unnamed", 0)
        {
            
        }

        public TestParameter(int value)
            : this("Unnamed", value)
        {
        }

        public TestParameter(string name)
            : this(name, 0)
        {
        }

        public TestParameter(string name, int value)
        {
            Name = name;
            Value = value;
        }
    }

    public interface ITestProperty
    {
        int Value { get; }
    }

    public class TestProperty : ITestProperty
    {
        public int Value { get; private set; }

        public TestProperty()
            : this(0)
        {
            
        }

        public TestProperty(int value)
        {
            Value = value;
        }
    }

    public class TestPropertyNoDefaultConstructor : ITestProperty
    {
        public int Value { get; private set; }

        public TestPropertyNoDefaultConstructor(int value)
        {
            Value = value;
        }
    }
}
