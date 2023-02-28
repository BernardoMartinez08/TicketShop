using Newtonsoft.Json;
using TicketShop.Gateway.Dtos.Basket;

namespace TicketShop.Gateway.Services;

public class ShoppingBasketService : IShoppingBasketService
{
    private readonly HttpClient _httpClient;

    public ShoppingBasketService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<BasketDataTransferObject> AddBasketAsync(CreateBasketDataTransferObject basket)
    {
        var result = await _httpClient.PostAsJsonAsync($"http://localhost:5075/ShoppingBaskets", basket);
        result.EnsureSuccessStatusCode();
        var response = await result.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<BasketDataTransferObject>(response);
    }

    public async Task<BasketDataTransferObject> GetBasketAsync(Guid basketId)
    {
        var result = await _httpClient.GetStringAsync($"http://localhost:5075/ShoppingBaskets/{basketId}");
        return JsonConvert.DeserializeObject<BasketDataTransferObject>(result);
    }
}