using System.Collections.Generic;
using ItemList.Contracts.DependencyInjection;

namespace ItemList.DependencyInjection.Builder
{
    public interface IBootstrappersBuilder
    {
        IBootstrappersBuilder Include<TRegistration>() 
            where TRegistration : IBootstrapper, new();

        IEnumerable<IBootstrapper> AsEnumerable();
    }
}
