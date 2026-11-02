using DomainLayer.Entities.Basket;
using DomainLayer.Repositories;
using StackExchange.Redis;
using System.Text.Json;

namespace Persistence.Repositories;

public class BasketRepository : IBasketRepository
{
    private readonly IDatabase _database;

    public BasketRepository(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }

    public async Task<CustomerBasket?> GetBasketAsync(string basketId)
    {
        var basket = await _database.StringGetAsync(basketId);

        return basket.IsNullOrEmpty 
            ? null
            : JsonSerializer.Deserialize<CustomerBasket>(basket.ToString());
    }

    public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
    {
        var created = await _database.StringSetAsync(
            basket.Id,
            JsonSerializer.Serialize(basket),
            TimeSpan.FromDays(30));

        if (!created)
            throw new Exception("Could not update basket");

        return await GetBasketAsync(basket.Id) ?? basket;
    }

    public async Task<bool> DeleteBasketAsync(string basketId)
    {
        return await _database.KeyDeleteAsync(basketId);
    }
}