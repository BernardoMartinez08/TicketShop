using TicketShop.Payments;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHostedService<PaymentService>();

var app = builder.Build();

app.Map("/", () => "Hello World!");

app.Run();
