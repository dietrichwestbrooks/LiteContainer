namespace LiteContainer
{
    internal interface IBuilderStage
    {
        void PreBuildUp(IBuilderContext context);

        void PostBuildUp(IBuilderContext context);

        void PreTearDown(IBuilderContext context);

        void PostTearDown(IBuilderContext context);
    }
}
