using DomainLayer.Entities.Basket;
using DomainLayer.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BasketsController(IBasketRepository basketRepository)
    : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
    {
        var basket = await basketRepository.GetBasketAsync(id);

        return basket is null ? NotFound() : Ok(basket);
    }

    [HttpPost]
    public async Task<ActionResult<CustomerBasket>> UpdateBasket(
        CustomerBasket basket)
    {
        var updatedBasket = await basketRepository.UpdateBasketAsync(basket);

        return Ok(updatedBasket);
    }

    [HttpDelete]
    public async Task<ActionResult<bool>> DeleteBasket(string id)
    {
        var deleted = await basketRepository.DeleteBasketAsync(id);

        return Ok(deleted);
    }
}