using System;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace POC_SignalR.Service
{
    /// <summary>
    ///     定義Redis Publish 所需要的 interface
    /// </summary>
    public interface IRedisNotificationService
    {
        /// <summary>
        ///     Asynchronous publish the specified channel.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="channel">The channel.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        Task<long> PublishAsync<T>(string channel, T data);

        /// <summary>
        ///     Asynchronous publish the specified channel.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <param name="serializeObject">The serialize object.</param>
        /// <returns></returns>
        Task<long> PublishAsync(string channel, string serializeObject);

        /// <summary>
        ///     Publishes the specified channel.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <param name="serializeObject">The serialize object.</param>
        /// <returns></returns>
        long Publish(string channel, string serializeObject);

        /// <summary>
        ///     Publishes the specified channel.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="channel">The channel.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        long Publish<T>(string channel, T data);
        /// <summary>
        ///     訂閱指定的 channel, 並執行指定的 action
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <param name="action">The action.</param>
        void Subscribe(RedisChannel channel, Action<RedisChannel, RedisValue> action);

        /// <summary>
        ///     以非同步的方式訂閱指定的 channel, 並執行指定的 action 
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        Task SubscribeAsync(RedisChannel channel, Action<RedisChannel, RedisValue> action);
    }
}
