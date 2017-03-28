using System.Security.Cryptography.X509Certificates;
using ItemList.Contracts.Api;

namespace ItemList.Contracts.Bootstrap
{
    public interface IBootstrapper
    {
        void RegisterTypes(IIoCContainer container);
    }
}