using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

using Microsoft.Framework.ConfigurationModel;

using Npgsql;

namespace SampleApp.EF
{
    public class MainContextFactory : IDbContextFactory<MainContext>
    {
        private readonly IConfiguration _configuration;
        
        public MainContextFactory()
        {
            _configuration = Startup.LoadConfiguration();
        }
        
        public MainContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public MainContext Create()
        {
            var connectionString = BuildConnectionString(_configuration);
            
            return new MainContext(connectionString);
        }
        
        public static String BuildConnectionString(IConfiguration configuration)
        {
            var builder = new NpgsqlConnectionStringBuilder();
            
            foreach (var kv in configuration.GetSubKeys("Database"))
            {
                builder[kv.Key] = kv.Value.Get(key: null);
            }
            
            return builder.ConnectionString;
        }
    }
}