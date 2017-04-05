using System;
using System.Collections.Generic;
using System.Configuration;
using ItemList.Contracts.Api;

namespace ItemList.Api
{
    public class ConnectionStrings : IConnectionStrings
    {
        private static Dictionary<string, string> _connectionString;

        public string GetConnectionString(string name)
        {
            string connectionString;
            if (!_connectionString.TryGetValue(name, out connectionString))
            {
                var connectionStringSettings = ConfigurationManager.ConnectionStrings[name];
                if (connectionStringSettings == null)
                {
                    throw new ArgumentException("Name not found in configuration.");
                }
                connectionString = connectionStringSettings.ConnectionString;
                _connectionString.Add(name, connectionString);
            }
            return connectionString;
        }
    }
}