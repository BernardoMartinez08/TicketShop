using TicketShop.Gateway.Dtos.Events;

namespace TicketShop.Gateway.Dtos.Categories
{
    public class CategoryDataTransferObject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<EventDataTransferObject> Events { get; set; }
    }
}
