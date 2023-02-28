using Microsoft.AspNetCore.Mvc;
using TicketShop.EventCatalog.Dtos;

namespace TicketShop.EventCatalog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : ControllerBase
    {
        private static IEnumerable<CategoryDataTransferObject> categories = new List<CategoryDataTransferObject> {
            new CategoryDataTransferObject
            {
                Id = Guid.Parse("4a9c948e-0855-43c2-8de4-da98b17a1c1a"),
                Name = "Concerts",
            },
            new CategoryDataTransferObject
            {
                Id = Guid.Parse("c63a952d-b2e9-4b7e-9ec9-309ec1b73e58"),
                Name = "Sports",
            }
        };

        [HttpGet]
        public IActionResult GetCategory() {
            return Ok(categories);
        }

        [HttpGet("{categoryId}")]
        public IActionResult GetCategoryById(Guid categoryId) {
            var category = categories.SingleOrDefault(x => x.Id == categoryId);
            return category is null
            ?
                NotFound($"No se encontro ca categoria con el id: {categoryId}")
            :
                Ok(category);
        }

    }
}
