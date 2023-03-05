using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TicketShop.Gateway.Settings;
using TicketShop.Gateway.Dtos;
using TicketShop.Gateway.Dtos.Basket;
using TicketShop.Gateway.Dtos.ShoppingBasketLines;

namespace TicketShop.Gateway.Services;

public class ShoppingBasketService : IShoppingBasketService
{
    private readonly IServiceRegistry _serviceRegistry;
    private readonly ApplicationSettings _appSettings;
    private readonly HttpClient _httpClient;

    public ShoppingBasketService(HttpClient httpClient, IServiceRegistry serviceRegistry, IOptions<ApplicationSettings> appSettings)
    {
        _serviceRegistry = serviceRegistry;
        _appSettings = appSettings.Value;
        _httpClient = httpClient;
    }

    public async Task<BasketDataTransferObject> AddBasketAsync(CreateBasketDataTransferObject basketToCreate)
    {
        var shoppingBasketsUrl = await this._serviceRegistry.GetService(_appSettings.ShoppingBasketServiceId);
        var result = await _httpClient.PostAsJsonAsync($"{shoppingBasketsUrl.Origin}/ShoppingBaskets",basketToCreate);
        result.EnsureSuccessStatusCode();
        var response = await result.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<BasketDataTransferObject>(response);
    }

    public async Task<BasketDataTransferObject> GetBasketAsync(Guid id)
    {
        var shoppingBasketsUrl = await this._serviceRegistry.GetService(_appSettings.ShoppingBasketServiceId);
        var result = await _httpClient.GetStringAsync($"{shoppingBasketsUrl.Origin}/ShoppingBaskets/{id}");
        return JsonConvert.DeserializeObject<BasketDataTransferObject>(result);
    }
}

