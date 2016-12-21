namespace Wayne.Payment.Platform
{
    internal interface IBuildPlanPolicy : IBuilderPolicy
    {
        object BuildUp(IBuilderContext context);
    }
}
