using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using SampleRedisandSignalR.RedisBase.Base;
using System.Text;
using System.Text.Json;

namespace SampleRedisandSignalR.RedisBase.Logic
{
    public class IndexManager : IIndexManager
    {
        private readonly RedisConnector _redisClient;

        public IndexManager(RedisConnector redisServer)
        {
            this._redisClient = redisServer;
        }
        public bool Any(string key)
        {
            throw new NotImplementedException();
        }

        public byte[] Get(string key)
        {
            var a = _redisClient.RedisCache.Get(key);
            return a;
        }

        public T Get<T>(string key)
        {
            var byteData = Encoding.UTF8.GetString(Get(key));
            var data = JsonConvert.DeserializeObject<T>(byteData);
            return data;
        }

        public void Refresh(string key)
        {
            _redisClient.RedisCache.Refresh(key);
        }

        public void Remove(string key)
        {
            _redisClient.RedisCache.Remove(key);
        }

        public void Set(string key, object value)
        {
            var serializedObject = System.Text.Json.JsonSerializer.Serialize(value);
            var utf8String = Encoding.UTF8.GetBytes(serializedObject);
            _redisClient.RedisCache.Set(key, utf8String);
        }
    }
}
