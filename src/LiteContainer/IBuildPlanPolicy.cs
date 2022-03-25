namespace LiteContainer
{
    internal interface IBuildPlanPolicy : IBuilderPolicy
    {
        object BuildUp(IBuilderContext context);
    }
}
