using System;
using System.Collections.Generic;
using System.Configuration;
using ItemList.Contracts.Api;

namespace ItemList.Api.Services
{
    public class DatabaseConfiguration : IDatabaseConfiguration
    {
        private readonly string _connectionString;

        public DatabaseConfiguration()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        public string GetConnectionString()
        {
            return _connectionString;
        }
    }
}