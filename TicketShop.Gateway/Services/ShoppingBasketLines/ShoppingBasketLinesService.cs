using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TicketShop.Gateway.Settings;
using TicketShop.Gateway.Dtos.ShoppingBasketLines;

namespace TicketShop.Gateway.Services;

public class ShoppingBasketLinesService : IShoppingBasketLinesService
{
    private readonly IServiceRegistry _serviceRegistry;
    private readonly ApplicationSettings _appSettings;
    private readonly HttpClient _httpClient;

    public ShoppingBasketLinesService(HttpClient httpClient, IServiceRegistry serviceRegistry, IOptions<ApplicationSettings> appSettings)
    {
        _serviceRegistry = serviceRegistry;
        _appSettings = appSettings.Value;
        _httpClient = httpClient;
    }
    public async Task<BasketLineDataTransferObject> AddBasketLineAsync(Guid basketId, CreateBasketLineDataTransferObject basketLine)
    {
        var shoppingBasketsUrl = await this._serviceRegistry.GetService(_appSettings.ShoppingBasketServiceId);
        var result = await
            _httpClient.PostAsJsonAsync($"{shoppingBasketsUrl.Origin}/ShoppingBaskets/{basketId}/BasketLines", basketLine);
        result.EnsureSuccessStatusCode();
        var response = await result.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<BasketLineDataTransferObject>(response);
    }

    public async Task<IEnumerable<BasketLineDataTransferObject>> GetBasketLinesAsync(Guid basketId)
    {
        var shoppingBasketsUrl = await _serviceRegistry.GetService(_appSettings.ShoppingBasketServiceId);
        var result = await _httpClient.GetStringAsync($"{shoppingBasketsUrl.Origin}/ShoppingBaskets/{basketId}/BasketLines");
        return JsonConvert.DeserializeObject<IEnumerable<BasketLineDataTransferObject>>(result);
    }
}
