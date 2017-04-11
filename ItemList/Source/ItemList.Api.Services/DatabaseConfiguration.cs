using System.Configuration;
using ItemList.Contracts.Api;

namespace ItemList.Api.Services
{
    public class DatabaseConfiguration : IDatabaseConfiguration
    {
        public string DefaultConnectionString { get; }

        public DatabaseConfiguration(string defaultConnectionString)
        {
            DefaultConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
    }
}