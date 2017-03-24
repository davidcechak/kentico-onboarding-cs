namespace ItemList.Contracts.Api
{
    public interface IIoCContainer
    {
        void RegisterRequestScope<TInterface, TImplementation>() where TImplementation : TInterface, new();
    }
}