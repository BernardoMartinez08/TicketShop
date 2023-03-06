using TicketShop.ShoppingBasket.Dtos.Basket;
using TicketShop.ShoppingBasket.Dtos.BasketLines;
using TicketShop.ShoppingBasket.Dtos.Event;

namespace TicketShop.ShoppingBasket.DataBases
{
    public static class DataBase
    {
        public static readonly List<BasketDataTransferObject> Baskets = new List<BasketDataTransferObject>{};

        public static readonly List<BasketLineDataTransferObject> BasketLines = new List<BasketLineDataTransferObject>();

        public static readonly List<EventDataTransferObject> Events = new();
    }
}
