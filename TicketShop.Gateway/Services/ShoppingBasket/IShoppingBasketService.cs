using TicketShop.Gateway.Dtos.Basket;

namespace TicketShop.Gateway.Services;

public interface IShoppingBasketService
{
    Task<BasketDataTransferObject> AddBasketAsync(CreateBasketDataTransferObject basket);
    Task<BasketDataTransferObject> GetBasketAsync(Guid basketId);
}