namespace ItemList.Contracts.Bootstrap
{
    public interface IIncludeBootstrapper<out TBootstrapper> 
        where TBootstrapper : IIncludeBootstrapper<TBootstrapper>
    {
        TBootstrapper Include<TRegistration>() where TRegistration : IBootstrapper, new();
    }
}
