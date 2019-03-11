using System;
using System.Configuration;
using StackExchange.Redis;

namespace POC_SignalR.App_Start
{
    public static class RedisHelper
    {
        private static readonly Lazy<ConnectionMultiplexer> ConnectionMultiplexer = new Lazy<ConnectionMultiplexer>(
            () =>
            {
                var connectionString =
                    ConfigurationManager.ConnectionStrings["RedisConnection"].ConnectionString;
                var options = ConfigurationOptions.Parse(connectionString);
                return StackExchange.Redis.ConnectionMultiplexer.Connect(options);
            });

        /// <summary>
        /// </summary>
        public static ConnectionMultiplexer Instance = ConnectionMultiplexer.Value;
    }
}