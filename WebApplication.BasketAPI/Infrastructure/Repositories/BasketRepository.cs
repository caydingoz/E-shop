using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WebApplication.BasketAPI.Model;

namespace WebApplication.BasketAPI.Infrastructure.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly ILogger<BasketRepository> _logger;
        private readonly IConnectionMultiplexer _redis;
        private readonly IDatabase _database;

        public BasketRepository(ILoggerFactory loggerFactory, IConnectionMultiplexer redis)
        {
            _logger = loggerFactory.CreateLogger<BasketRepository>();
            _redis = redis;
            _database = redis.GetDatabase();
        }
        public IEnumerable<string> GetUsers()
        {
            var server = GetServer();
            var data = server.Keys(); //redis key value oldugundan keylerde user oldugundan bu sekilde cektik

            return data?.Select(k => k.ToString());
        }

        public async Task<bool> DeleteBasketAsync(int customerId)
        {
            return await _database.KeyDeleteAsync(customerId.ToString());
        }

        public async Task<CustomerBasket> GetBasketAsync(int customerId)
        {
            var data = await _database.StringGetAsync(customerId.ToString());

            if (data.IsNullOrEmpty)
            {
                return null;
            }

            return JsonSerializer.Deserialize<CustomerBasket>(data, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var created = await _database.StringSetAsync(basket.BuyerId.ToString(), JsonSerializer.Serialize(basket));

            if (!created)
            {
                _logger.LogInformation("Problem occur persisting the item.");
                return null;
            }

            _logger.LogInformation("Basket item persisted succesfully.");

            return await GetBasketAsync(basket.BuyerId);
        }
        private IServer GetServer() //serveri aldik
        {
            var endpoint = _redis.GetEndPoints();
            return _redis.GetServer(endpoint.First());
        }
    }
}
