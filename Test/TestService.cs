using System;

namespace LiteContainer.Test
{
    public interface ITestService
    {
        int Value { get; }
        ITestParameter Parameter { get; }
    }

    public class TestService : ITestService
    {
        public int Value { get; private set; }
        public ITestParameter Parameter { get; private set; }

        public TestService()
        {
            
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
    }

    public interface ITestParameter
    {
        int Value { get; }
    }

    public class TestParameter : ITestParameter
    {
        public int Value { get; private set; }

        public TestParameter()
            : this(0)
        {
            
        }

        public TestParameter(int value)
        {
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
}
