using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Options;
using SampleRedisandSignalR.RedisBase.Enums;
using StackExchange.Redis;

namespace SampleRedisandSignalR.RedisBase.Base
{
    public class RedisConnector
    {
        #region Members
        public RedisCache RedisCache;

        #endregion

        #region Constractor

        public RedisConnector()
        {
            var redisOptions = new RedisCacheOptions
            {
                ConfigurationOptions = new ConfigurationOptions
                {
                    EndPoints = { { "localhost", 30007} },
                    AbortOnConnectFail = false
                }
            };

            var opts = Options.Create(redisOptions);
            RedisCache = new RedisCache(opts);
        }

        #endregion

    }
}
