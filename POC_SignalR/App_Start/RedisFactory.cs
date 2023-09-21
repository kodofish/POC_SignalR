using System.Configuration;
using StackExchange.Redis;

namespace POC_SignalR
{
    public static class RedisFactory
    {
        private static readonly ConnectionMultiplexer _instance;

        static RedisFactory()
        {
            var connectionString =
                ConfigurationManager.ConnectionStrings["RedisConnection"].ConnectionString;
            var options = ConfigurationOptions.Parse(connectionString);
            _instance = ConnectionMultiplexer.Connect(options);
        }

        public static ConnectionMultiplexer Instance => _instance;
    }
}