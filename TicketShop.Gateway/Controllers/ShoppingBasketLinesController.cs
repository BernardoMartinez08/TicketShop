using Microsoft.AspNetCore.Mvc;
using TicketShop.Gateway.Dtos.ShoppingBasketLines;
using TicketShop.Gateway.Services;

namespace TicketShop.Gateway.Controllers;

[ApiController]
[Route("[controller]")]
public class ShoppingBasketLinesController : ControllerBase
{
    private readonly IShoppingBasketLinesService _shoppingBasketService;

    public ShoppingBasketLinesController(IShoppingBasketLinesService shoppingBasketService)
    {
        _shoppingBasketService = shoppingBasketService;
    }

    [HttpPost("/ShoppingBaskets/{basketId}/ShoppingBasketLines")]
    public async Task<IActionResult> Post(Guid basketId, CreateBasketLineDataTransferObject lineToCreate)
    {
        var result = await this._shoppingBasketService.AddBasketLineAsync(basketId, lineToCreate);
        return Ok(result);
    }

    [HttpGet("/ShoppingBaskets/{basketId}/ShoppingBasketLines")]
    public async Task<IActionResult> Post(Guid basketId)
    {
        var result = await this._shoppingBasketService.GetBasketLinesAsync(basketId);
        return Ok(result);
    }
}