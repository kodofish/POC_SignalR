using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Newtonsoft.Json;
using POC_SignalR;
using POC_SignalR.Hub;
using POC_SignalR.Service;
using StackExchange.Redis;

[assembly: OwinStartup(typeof(Startup))]

namespace POC_SignalR
{
    public partial class Startup
    {
        private IRedisNotificationService _subscriber;
        private IHubContext _mainHub;

        /// <summary>
        ///     Initials the Service
        /// </summary>
        private void InitialService()
        {
            IConnectionMultiplexer connectionMultiplexer = RedisFactory.Instance;
            _subscriber = new RedisNotificationService(connectionMultiplexer);
            _mainHub = GlobalHost.ConnectionManager.GetHubContext<MainHub>();
        }

        /// <summary>
        ///     初始化 Redis 設定
        /// </summary>
        private void ConfigurationRedis()
        {
            InitialService();

            // 訂閱: 訊息通知 (value: <BonusNotification>)
            var redisChannel = new RedisChannel("ChatMessage", RedisChannel.PatternMode.Auto);
            _subscriber.Subscribe(redisChannel, (channel, value) =>
            {
                var chatMessage = JsonConvert.DeserializeObject<ChatMessage>(value);
                _mainHub.Clients.All.broadcastMessage(chatMessage.Name, chatMessage.Message);
            });
        }
    }
}