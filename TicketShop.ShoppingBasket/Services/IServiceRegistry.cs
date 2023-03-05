using TicketShop.ShoppingBasket.Dtos.Registry;

namespace TicketShop.ShoppingBasket.Services;

public interface IServiceRegistry
{
    Task<ServiceRegistryDataTransferObject> GetService(string id);
    Task AddService(ServiceRegistryDataTransferObject service);
}
