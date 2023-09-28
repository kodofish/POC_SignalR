using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
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
            _connectionWrapper = connectionMultiplexer ?? throw new ArgumentNullException(nameof(connectionMultiplexer),"Redis connection multiplexer is null.");
        }

        /// <inheritdoc />
        public async Task<long> PublishAsync<T>(string channel, T data)
        {
            var serializeJson = JsonConvert.SerializeObject(data);
            return await PublishAsync(channel, serializeJson);
        }

        /// <inheritdoc />
        public async Task<long> PublishAsync(string channel, string serializeObject)
        {
            var subscriber = _connectionWrapper.GetSubscriber();
            var redisChannel = new RedisChannel(channel, RedisChannel.PatternMode.Auto);
            var published = await subscriber.PublishAsync(redisChannel, serializeObject);
            return published;
        }

        /// <inheritdoc />
        public long Publish(string channel, string serializeObject)
        {
            var subscriber = _connectionWrapper.GetSubscriber();
            var redisChannel = new RedisChannel(channel, RedisChannel.PatternMode.Auto);
            var published = subscriber.Publish(redisChannel, serializeObject);
            return published;
        }

        /// <inheritdoc />
        public long Publish<T>(string channel, T data)
        {
            var serializeJson = JsonConvert.SerializeObject(data);
            var redisChannel = new RedisChannel(channel, RedisChannel.PatternMode.Auto);
            return Publish(redisChannel, serializeJson);
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