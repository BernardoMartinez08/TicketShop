using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TicketShop.ShoppingBasket.DataBases;
using TicketShop.ShoppingBasket.Dtos.BasketLines;
using TicketShop.ShoppingBasket.Dtos.Event;

namespace TicketShop.ShoppingBasket.Controllers
{
    [ApiController]
    [Route("[controller")]
    public class ShoppingBasketLinesController : ControllerBase
    {
        [HttpGet("/ShoppingBaskets/{basketId}/BasketLines")]
        public IActionResult Get(Guid basketId)
        {
            var basket = DataBase.Baskets.SingleOrDefault(x => x.BasketId == basketId);

            if (basket is null)
            {
                return NotFound($"No se encontro el basket con id {basketId}"); ;
            }

            var basketLines = DataBase.Baskets.Where(x => x.BasketId == basketId);
            return Ok(basketLines);
        }

        [HttpPost("/ShoppingBaskets/{basketId}/BasketLines")]
        public async Task<IActionResult> Post(Guid basketId, [FromBody] CreateBasketLineDataTransferObject basketLineToCreate)
        {
            var basket = DataBase.Baskets.SingleOrDefault(x => x.BasketId == basketId);
            if (basket is null)
            {
                return NotFound($"No se encontró el basket con id {basketId}");
            }

            var eventData = DataBase.Events.SingleOrDefault(x => x.EventId == basketLineToCreate.EventId);
            if (eventData is null)
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetStringAsync($"http://localhost:5069/events/{basketLineToCreate.EventId}");
                    eventData = JsonConvert.DeserializeObject<EventDataTransferObject>(response);
                    DataBase.Events.Add(eventData);
                }
            }

            if (eventData is null)
            {
                return NotFound($"No se encontró un evento con el id: {basketLineToCreate.EventId}");
            }

            var line = new BasketLineDataTransferObject
            {
                BasketId = basketId,
                BasketLineId = Guid.NewGuid(),
                EventId = basketLineToCreate.EventId,
                TicketQuantity = basketLineToCreate.TicketQuantity,
                Price = eventData.Price * basketLineToCreate.TicketQuantity
            };

            DataBase.BasketLines.Add(line);
            return Ok(line);
        }
    }


}
