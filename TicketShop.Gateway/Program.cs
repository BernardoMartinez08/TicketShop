using Microsoft.AspNetCore.Mvc;
using TicketShop.Gateway.Dtos;
using TicketShop.Gateway.Services;
using TicketShop.Gateway.Services.Event;
using TicketShop.Gateway.Settings;

[assembly: ApiController]

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<ApplicationSettings>(builder.Configuration.GetSection("ApplicationSettings"));
builder.Services.AddHttpClient<IServiceRegistry, ServiceRegistry>();
builder.Services.AddControllers();
builder.Services.AddHttpClient<IEventService, EventService>();
builder.Services.AddHttpClient<ICategoryService, CategoryService>();
builder.Services.AddHttpClient<IShoppingBasketService, ShoppingBasketService>();
builder.Services.AddHttpClient<IShoppingBasketLinesService, ShoppingBasketLinesService>();
var app = builder.Build();

app.MapControllers();

var serviceRegistry = app.Services.GetService<IServiceRegistry>();
await serviceRegistry.AddService(new ServiceRegistryDataTransferObject
{
    Service = "ticketshop.gateway",
    Origin = "http://localhost:5203"
});

app.Run();