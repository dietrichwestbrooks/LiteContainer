namespace Wayne.Payment.Platform.Lite
{
    //internal sealed class Builder
    //{
    //    private ILiteContainer _container;

    //    public Builder(ILiteContainer container)
    //    {
    //        _container = container;
    //    }

    //    public object NewBuildUp(BuildKey key, params IResolveParameterPolicy[] resolverPolicies)
    //    {
    //        if (!_registeredServices.ContainsKey(key))
    //            return null;

    //        var policies = _registeredServices[key];
    //        policies.AddRange(resolverPolicies);

    //        return NewBuildUp(key, policies);
    //    }

    //    public object NewBuildUp(BuildKey key, IList<IBuilderPolicy> policies)
    //    {
    //        var lifetimePolicy = policies.OfType<ILifetimePolicy>().Single();

    //        object service = lifetimePolicy.GetValue();

    //        if (service != null)
    //            return service;

    //        var context = new BuilderContext(this, key, policies, _lifetime);

    //        var buildPlanPolicy = policies.OfType<IBuildPlanPolicy>().Single();

    //        return buildPlanPolicy.BuildUp(context);
    //    }

    //    private object BuildUp(BuildKey key, object service)
    //    {
    //        if (!_registeredServices.ContainsKey(key))
    //            return service;

    //        IList<IBuilderPolicy> policies = _registeredServices[key];

    //        var context = new BuilderContext(this, key, policies, _lifetime)
    //        {
    //            Current = service
    //        };

    //        var injectionPolicies = policies.OfType<IInjectionPolicy>();

    //        foreach (var injectionPolicy in injectionPolicies)
    //        {
    //            injectionPolicy.Inject(context);
    //        }

    //        return service;
    //    }
    //}
}
