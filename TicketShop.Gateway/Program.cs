using Microsoft.AspNetCore.Mvc;
using TicketShop.Gateway.Services;
using TicketShop.Gateway.Services.Event;

[assembly: ApiController]

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddHttpClient<IEventService, EventService>();
builder.Services.AddHttpClient<ICategoryService, CategoryService>();
builder.Services.AddHttpClient<IShoppingBasketService, ShoppingBasketService>();
builder.Services.AddHttpClient<IShoppingBasketLinesService, ShoppingBasketLinesService>();
var app = builder.Build();

app.MapControllers();
app.Run();