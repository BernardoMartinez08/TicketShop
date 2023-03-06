using Microsoft.AspNetCore.Mvc;
using TicketShop.EventCatalog.Dtos;

namespace TicketShop.EventCatalog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventsController : ControllerBase
    {
        private static IEnumerable<EventDataTransferObject> events = new List<EventDataTransferObject> {
            new EventDataTransferObject
            {
                EventId = Guid.NewGuid(),
                Date = DateTime.Now,
                Name = "Beyonce in Concert",
                Artist = "Beyonce",
                Price = 1000,
                CategoryId = Guid.Parse("4a9c948e-0855-43c2-8de4-da98b17a1c1a"),
                CategoryName = "Concerts",
            },
            new EventDataTransferObject
            {
                EventId = Guid.NewGuid(),
                Date = DateTime.Now.AddMonths(1),
                Name = "Ferxo in Concert",
                Artist = "Ferxo",
                Price = 500,
                CategoryId = Guid.Parse("4a9c948e-0855-43c2-8de4-da98b17a1c1a"),
                CategoryName = "Concerts",
            },
            new EventDataTransferObject
            {
                EventId = Guid.NewGuid(),
                Date = DateTime.Now.AddMonths(2),
                Name = "El Tongo in Concert",
                Artist = "El Tongo",
                Price = 100,
                CategoryId = Guid.Parse("4a9c948e-0855-43c2-8de4-da98b17a1c1a"),
                CategoryName = "Concerts",
            },
            new EventDataTransferObject
            {
                EventId = Guid.NewGuid(),
                Date = DateTime.Now.AddMonths(4),
                Name = "Coldplay in Concert",
                Artist = "Coldplay",
                Price = 800,
                CategoryId = Guid.Parse("4a9c948e-0855-43c2-8de4-da98b17a1c1a"),
                CategoryName = "Concerts",
            },
            new EventDataTransferObject
            {
                EventId = Guid.NewGuid(),
                Date = DateTime.Now.AddMonths(4),
                Name = "Olimpia vs Marathon",
                Artist = "None",
                Price = 100,
                CategoryId = Guid.Parse("c63a952d-b2e9-4b7e-9ec9-309ec1b73e58"),
                CategoryName = "Sports",
            }
        };

        [HttpGet]
        /*public IActionResult GetEvents() {
            return Ok(events);
        }*/


        [HttpGet]
        public IActionResult GetEvents([FromQuery] Guid? categoryId, [FromQuery] string? name)
        {
            var currentEvents = new List<EventDataTransferObject>();

            if (categoryId is null && name is null)
            {
                return Ok(events);
            }

            if (name is not null)
            {
                currentEvents = events.Where(@event => @event.Name.Contains(name)).ToList();
            }

            if (categoryId is not null)
            {
                currentEvents = currentEvents.Any()
                    ?
                        currentEvents.Where(@event => @event.CategoryId == categoryId).ToList()
                    :
                        events.Where(@event => @event.CategoryId == categoryId).ToList();
            }

            return !currentEvents.Any()
                ?
                    NotFound($"No se encontro el evento con el id de categoria: {categoryId} y nombre: {name}")
                :
                    Ok(currentEvents);
        }

        [HttpGet("{eventId}")]
        public IActionResult GetEventsById(Guid eventId)
        {
            var @event = events.SingleOrDefault(x => x.EventId == eventId);
            return @event is null
            ?
                NotFound($"No se encontro el evento con el id: {eventId}")
            :
                Ok(@event);
        }
    }
}
