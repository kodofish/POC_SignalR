﻿using LevelUp.Serializer;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using POC_SignalR.App_Start;
using POC_SignalR.Hub;
using POC_SignalR.Service;
using StackExchange.Redis;
using Startup = POC_SignalR.Startup;

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
            IConnectionMultiplexer connectionMultiplexer = RedisHelper.Instance;
            _subscriber = new RedisNotificationService(connectionMultiplexer);
            _mainHub = GlobalHost.ConnectionManager.GetHubContext<MainHub>();
        }

        /// <summary>
        ///     初始化 Redis 設定
        /// </summary>
        public void ConfigurationRedis()
        {
            InitialService();

            // 訂閱: 訊息通知 (value: <BonusNotification>)
            _subscriber.Subscribe("ChatMessage", (channel, value) =>
            {
                ChatMessage chatMessage = Serializer.DeSerializeFromText<ChatMessage>(value, SerializerType.Json);
                _mainHub.Clients.All.broadcastMessage(chatMessage.Name, chatMessage.Message);
            });
        }
    }
}