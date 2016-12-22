using System.Collections.Generic;

<<<<<<< HEAD
namespace Wayne.Payment.Platform.Lite
{
    public class NamedParameter<TValue> : ResolveParameter
    {
        private string _name;
        private TValue _value;

        public NamedParameter(string name, TValue value)
        {
            _name = name;
            _value = value;
=======
namespace Wayne.Payment.Platform
{
    public class NamedParameter : ResolveParameter
    {
        private string _paramName;
        private object _paramValue;

        public NamedParameter(string paramName, object paramValue)
        {
            _paramName = paramName;
            _paramValue = paramValue;
>>>>>>> 06f38426eb2a120e3f5be0a79f2c3cf88f9ff4e4
        }

        internal override void AddPolicies(IList<IBuilderPolicy> policies)
        {
<<<<<<< HEAD
            policies.Add(new NamedParameterPolicy(_name, _value));
=======
            policies.Add(new NamedParameterPolicy(_paramName, _paramValue));
>>>>>>> 06f38426eb2a120e3f5be0a79f2c3cf88f9ff4e4
        }
    }
}
