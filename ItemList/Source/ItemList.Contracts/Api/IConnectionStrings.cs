using System.Collections.Generic;

namespace ItemList.Contracts.Api
{
    public interface IConnectionStrings
    {
        string GetConnectionString(string name);
    }
}