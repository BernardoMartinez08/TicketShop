using TicketShop.Gateway.Dtos.Events;

namespace TicketShop.Gateway.Services.Event;

public interface IEventService
{
    Task<IEnumerable<EventDataTransferObject>> GetEventsAsync(Guid? categoryId, string? name);

    Task<EventDataTransferObject> GetEventAsync(Guid eventId);
}