namespace Wayne.Payment.Platform.Lite
{
    internal interface IBuildPlanPolicy : IBuilderPolicy
    {
        object BuildUp(IBuilderContext context);
    }
}
