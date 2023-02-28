namespace TicketShop.ShoppingBasket.Dtos.Event
{
    public class EventDataTransferObject
    {
        public Guid EventId { get; set; }

        public decimal Price { get; set; }

        public string Name { get; set; }
    }
}
