using TicketShop.Gateway.Dtos.Categories;

namespace TicketShop.Gateway.Services;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDataTransferObject>> GetAsync();
    Task<CategoryDataTransferObject> GetAsync(Guid id);
}