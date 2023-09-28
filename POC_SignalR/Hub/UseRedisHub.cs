using System;
using System.Threading.Tasks;
using POC_SignalR.Service;
using StackExchange.Redis;

namespace POC_SignalR.Hub
{
    public sealed class UseRedisHub : Microsoft.AspNet.SignalR.Hub<IMainHubClient>
    {
        private readonly ConnectionMultiplexer _redisConnection = RedisFactory.Instance;

        private readonly RedisNotificationService _redisNotification;

        public UseRedisHub()
        {
            _redisNotification = new RedisNotificationService(_redisConnection);
        }

        public void Hello()
        {
            Clients.All.hello();
        }

        /// <summary>
        ///     Sends the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="message">The message.</param>
        public async Task Send(string name, string message)
        {
            var chatMessage = new ChatMessage
            {
                Name = name,
                Message = message,
                Date = DateTime.Now
            };

            await _redisNotification.PublishAsync("ChatMessage", chatMessage);
        }
    }
    
    public interface IMainHubClient
    {
        void hello();
    }
}