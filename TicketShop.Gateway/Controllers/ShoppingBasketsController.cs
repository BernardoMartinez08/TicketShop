using Microsoft.AspNetCore.Mvc;
using TicketShop.Gateway.Dtos.Basket;
using TicketShop.Gateway.Services;

namespace TicketShop.Gateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShoppingBasketsController : ControllerBase
    {
        private readonly IShoppingBasketService _shoppingBasketService;
        
        public ShoppingBasketsController(IShoppingBasketService shoppingBasketService)
        {
            _shoppingBasketService = shoppingBasketService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateBasketDataTransferObject basket)
        {
            var result = await this._shoppingBasketService.AddBasketAsync(basket);
            return Ok(result);
        }

        [HttpGet ("{basketId}")]
        public async Task<IActionResult> Get(Guid basketId)
        {
            var result = await this._shoppingBasketService.GetBasketAsync(basketId);
            return Ok(result);
        }   
    }
}
