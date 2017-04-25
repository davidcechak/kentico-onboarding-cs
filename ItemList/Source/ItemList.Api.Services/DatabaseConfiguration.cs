using System.Configuration;
using ItemList.Contracts.Api;

namespace ItemList.Api.Services
{
    public class DatabaseConfiguration : IDatabaseConfiguration
    {
        public string DefaultConnectionString { get; }

        public DatabaseConfiguration()
        {
            DefaultConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
    }
}