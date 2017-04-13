namespace ItemList.DependencyInjection.Builder
{
    internal interface IResolverBuilder : IResolverBuilderInitializer
    {
        void RegisterDependencyResolver();
    }
}