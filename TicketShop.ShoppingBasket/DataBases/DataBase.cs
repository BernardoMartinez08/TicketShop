using TicketShop.ShoppingBasket.Dtos.Basket;
using TicketShop.ShoppingBasket.Dtos.BasketLines;
using TicketShop.ShoppingBasket.Dtos.Event;

namespace TicketShop.ShoppingBasket.DataBases
{
    public static class DataBase
    {
        public static readonly List<BasketDataTransferObject> Baskets = new List<BasketDataTransferObject>
        {
            new BasketDataTransferObject {
                BasketId = Guid.NewGuid(),
                UserId = Guid.Parse("b0dfe8f2-88d5-40c2-bb93-4b6f389d1be5"),
                NumberOfItems = 0,
            }

        };

        public static readonly List<BasketLineDataTransferObject> BasketLines = new List<BasketLineDataTransferObject>();

        public static readonly List<EventDataTransferObject> Events = new();
    }
}
