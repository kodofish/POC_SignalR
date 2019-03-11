using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Newtonsoft.Json;
using POC_SignalR.App_Start;

[assembly: OwinStartup(typeof(Startup))]

namespace POC_SignalR.App_Start
{
    public partial class Startup
    {
        //private IStrategy _redisStrategy;
        //private IRedisNotificationService _subscriber;

        /// <summary>
        ///     Initials the Service
        /// </summary>
        private void InitialService()
        {
            //_redisStrategy = Singleton<IEngine>.Instance.Resolve<IStrategy>();
            //_subscriber = Singleton<IEngine>.Instance.Resolve<IRedisNotificationService>();
        }

        /// <summary>
        ///     初始化 Redis 設定
        /// </summary>
        public void ConfigurationRedis()
        {
            InitialService();

            // 訂閱: 訊息通知 (value: <BonusNotification>)
            //_subscriber.Subscribe("PushBonus", (channel, value) =>
            //{
            //    _redisStrategy.SubscribePushBonus(channel, value);
            //    _redisStrategy.SubscribePushBonusForWebSubscribe(channel, value);
            //});
        }
    }

    
}