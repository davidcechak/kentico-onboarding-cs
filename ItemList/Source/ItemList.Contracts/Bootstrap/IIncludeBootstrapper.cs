namespace ItemList.Contracts.Bootstrap
{
    interface IIncludeBootstrapper<out TBootstrapper> 
        where TBootstrapper : IIncludeBootstrapper<TBootstrapper>
    {
        TBootstrapper Include<TRegistration>() where TRegistration : IBootstrapper, new();
    }
}
