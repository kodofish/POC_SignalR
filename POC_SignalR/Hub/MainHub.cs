using System;
using System.Threading.Tasks;
using POC_SignalR.App_Start;
using POC_SignalR.Service;
using StackExchange.Redis;

namespace POC_SignalR.Hub
{
    public class MainHub : Microsoft.AspNet.SignalR.Hub
    {
        private readonly ConnectionMultiplexer _redisConnection = RedisHelper.Instance;
        private RedisNotificationService _redisNotification;

        public RedisNotificationService RedisNotification
        {
            get => _redisNotification ?? (_redisNotification = new RedisNotificationService(_redisConnection));
            set => _redisNotification = value;
        }

        public void Hello()
        {
            Clients.All.hello();
        }

        public override Task OnConnected()
        {
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            return base.OnReconnected();
        }

        #region "Client Event"

        /// <summary>
        ///     Sends the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="message">The message.</param>
        public void Send(string name, string message)
        {
            RedisNotification = new RedisNotificationService(_redisConnection);
            var chatMessage = new ChatMessage
            {
                Name = name,
                Message = message,
                Date = DateTime.Now
            };
             RedisNotification.PublishAsync("ChatMessage", chatMessage);
        }

        #endregion
    }
}