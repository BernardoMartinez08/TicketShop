using Newtonsoft.Json;
using TicketShop.Gateway.Dtos.ShoppingBasketLines;

namespace TicketShop.Gateway.Services;

public class ShoppingBasketLinesService : IShoppingBasketLinesService
{
    private readonly HttpClient _httpClient;

    public ShoppingBasketLinesService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<BasketLineDataTransferObject> AddBasketLineAsync(Guid basketId, CreateBasketLineDataTransferObject basketLine)
    {
        var result = await _httpClient.PostAsJsonAsync($"http://localhost:5075/ShoppingBaskets/{basketId}/BasketLines", basketLine);
        result.EnsureSuccessStatusCode();
        var response = await result.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<BasketLineDataTransferObject>(response);
    }

    public async Task<IEnumerable<BasketLineDataTransferObject>> GetBasketLinesAsync(Guid basketId)
    {
        var result = await _httpClient.GetStringAsync($"http://localhost:5075/ShoppingBaskets/{basketId}/BasketLines");
        return JsonConvert.DeserializeObject<IEnumerable<BasketLineDataTransferObject>>(result);
    }
}