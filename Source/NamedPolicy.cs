namespace LiteContainer
{
    internal class NamedPolicy : INamedPolicy
    {
        public NamedPolicy()
        {
            Name = string.Empty;
        }

        public NamedPolicy(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}
