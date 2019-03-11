using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using LevelUp.Serializer;
using StackExchange.Redis;

namespace POC_SignalR.Service
{
    /// <summary>
    /// 實作 Redis 傳送訂閱訊息
    /// </summary>
    /// <seealso cref="IRedisNotificationService" />
    /// <summary>
    /// 實作 Redis 傳送訂閱訊息
    /// </summary>
    /// <seealso cref="IRedisNotificationService" />
    public class RedisNotificationService : IRedisNotificationService
    {
        private readonly IConnectionMultiplexer _connectionWrapper;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RedisNotificationService" /> class.
        /// </summary>
        /// <param name="connectionMultiplexer">The connection multiplexer.</param>
        public RedisNotificationService(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionWrapper = connectionMultiplexer ?? throw new Exception("Redis connection multiplexer is null.");
        }

        /// <inheritdoc />
        public async Task<long> PublishAsync<T>(string channel, T data)
        {
            var serializeJson = data.ToJSON();
            return await PublishAsync(channel, serializeJson);
        }

        /// <inheritdoc />
        public async Task<long> PublishAsync(string channel, string serializeObject)
        {
            var subscriber = _connectionWrapper.GetSubscriber();
            var published = await subscriber.PublishAsync(channel, serializeObject);
            return published;
        }

        /// <inheritdoc />
        public long Publish(string channel, string serializeObject)
        {
            var subscriber = _connectionWrapper.GetSubscriber();
            var published = subscriber.Publish(channel, serializeObject);
            return published;
        }

        /// <inheritdoc />
        public long Publish<T>(string channel, T data)
        {
            var serializeJson = data.ToJSON();
            return Publish(channel, serializeJson);
        }

        /// <inheritdoc />
        public void Subscribe(RedisChannel channel, Action<RedisChannel, RedisValue> action)
        {
            var subscriber = _connectionWrapper.GetSubscriber();
            subscriber.Subscribe(channel, action);
        }

        /// <inheritdoc />
        public async Task SubscribeAsync(RedisChannel channel, Action<RedisChannel, RedisValue> action)
        {
            var subscriber = _connectionWrapper.GetSubscriber();
            await subscriber.SubscribeAsync(channel, action);
        }
    }
}