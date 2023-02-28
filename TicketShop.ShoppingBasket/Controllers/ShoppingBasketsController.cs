using Microsoft.AspNetCore.Mvc;
using TicketShop.ShoppingBasket.DataBases;
using TicketShop.ShoppingBasket.Dtos.Basket;

namespace TicketShop.ShoppingBasket.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShoppingBasketsController : ControllerBase
    {

        [HttpPost]
        public IActionResult Post([FromBody] BasketDataTransferObject basket)
        {
            if(DataBase.Baskets.Any(x => x.UserId == basket.UserId))
            {
                return BadRequest($"Ya existe un basket para este usuario: {basket.UserId}");
            }

            var basketDto = new BasketDataTransferObject
            {
                BasketId = Guid.NewGuid(),
                UserId = basket.UserId,
                NumberOfItems = basket.NumberOfItems,
            };

            DataBase.Baskets.Add(basketDto);

            return Ok(basketDto);
        }

        [HttpGet ("{basketId}")]
        public IActionResult Get(Guid basketId)
        {
            var basket = DataBase.Baskets.SingleOrDefault(x => x.BasketId == basketId);
            return basket is null
            ?
                NotFound($"No se encontro un carrito de compras con el id {basketId} ")
            :
                Ok(basket);
        }   
    }
}
