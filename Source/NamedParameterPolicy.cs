namespace Wayne.Payment.Platform.Lite
{
    internal class NamedParameterPolicy : IResolveParameterPolicy
    {
        public NamedParameterPolicy(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; private set; }
        public object Value { get; private set; }
    }
}
