using System.Collections.Generic;

namespace ItemList.Contracts.Api
{
    public interface IDatabaseConfiguration
    {
        string GetConnectionString();
    }
}