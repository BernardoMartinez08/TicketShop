using TicketShop.Gateway.Dtos.ShoppingBasketLines;

namespace TicketShop.Gateway.Services;

public interface IShoppingBasketLinesService
{
    Task<BasketLineDataTransferObject> AddBasketLineAsync(Guid basketId, CreateBasketLineDataTransferObject basketLine);
    Task<IEnumerable<BasketLineDataTransferObject>> GetBasketLinesAsync(Guid basketId);
}