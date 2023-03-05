using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TicketShop.ShoppingBasket.Dtos.BasketLines;
using TicketShop.ShoppingBasket.Dtos.Event;
using TicketShop.ShoppingBasket.Services;
using TicketShop.ShoppingBasket.Settings;

namespace TicketShop.ShoppingBasket;

[ApiController]
[Route("[controller]")]
public class ShoppingBasketLinesController : ControllerBase
{
    private readonly IServiceRegistry _serviceRegistry;
    private readonly ApplicationSettings _applicationSettings;
    public ShoppingBasketLinesController(IServiceRegistry serviceRegistry, IOptions<ApplicationSettings> appSettings)
    {
        _serviceRegistry = serviceRegistry;
        _applicationSettings = appSettings.Value;
    }
    [HttpGet("/ShoppingBaskets/{basketId}/BasketLines")]
    public IActionResult Get(Guid basketId)
    {
        var basket = DataBases.DataBase.Baskets.SingleOrDefault(x => x.BasketId == basketId);
        if (basket is null)
        {
            return NotFound($"No se encontró el basket con id {basketId}");
        }

        var basketLines = DataBases.DataBase.BasketLines.Where(x => x.BasketId == basketId);
        return Ok(basketLines);
    }

    [HttpPost("/ShoppingBaskets/{basketId}/BasketLines")]
    public async Task<IActionResult> Post(Guid basketId, [FromBody] CreateBasketLineDataTransferObject basketLineToCreate)
    {
        var basket = DataBases.DataBase.Baskets.SingleOrDefault(x => x.BasketId == basketId);
        if (basket is null)
        {
            return NotFound($"No se encontró el basket con id {basketId}");
        }

        var eventData = DataBases.DataBase.Events.SingleOrDefault(x => x.EventId == basketLineToCreate.EventId);
        if (eventData is null)
        {
            using (var httpClient = new HttpClient())
            {
                var eventCatalogUrl = await this._serviceRegistry.GetService(_applicationSettings.EventCatalogServiceId);
                var response = await httpClient.GetStringAsync($"{eventCatalogUrl.Origin}/events/{basketLineToCreate.EventId}");
                eventData = JsonConvert.DeserializeObject<EventDataTransferObject>(response);
                DataBases.DataBase.Events.Add(eventData);
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

        DataBases.DataBase.BasketLines.Add(line);
        return Ok(line);
    }
}