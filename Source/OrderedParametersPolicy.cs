namespace LiteContainer
{
    public class OrderedParametersPolicy : IResolveParameterPolicy
    {
        public OrderedParametersPolicy(object[] parameters)
        {
            Parameters = parameters;
        }

        public object[] Parameters { get; private set; }
    }
}
