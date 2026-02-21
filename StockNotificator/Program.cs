using Microsoft.EntityFrameworkCore;
using Quartz;
using StockNotificator.Application.Interfaces.Repositories;
using StockNotificator.Application.Interfaces.Services;
using StockNotificator.Application.Providers;
using StockNotificator.Application.Services;
using StockNotificator.Configuration.Quartz;
using StockNotificator.Infraestructure.Context;
using StockNotificator.Infraestructure.Notification;
using StockNotificator.Infraestructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureQuartz();

builder.Services.AddHttpClient("Brapi", client =>
{
    client.BaseAddress = new Uri("https://brapi.dev/");
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Application Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserStockService, UserStockService>();
builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddScoped<IStockQuoteService, StockQuoteService>();
builder.Services.AddScoped<IAlertConditionService, AlertConditionService>();
builder.Services.AddScoped<INotificationService, NotificationService>();

// Add Infraestructures
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserStockRepository, UserStockRepository>();
builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<IStockQuoteRepository, StockQuoteRepository>();
builder.Services.AddScoped<IAlertConditionRepository, AlertConditionRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailService, EmailService>();

// Add Providers
builder.Services.AddScoped<QuoteProvider>();

builder.Services
    .AddControllers()
    .AddJsonOptions(options => 
    {
        options.JsonSerializerOptions.Converters
        .Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
        }
    );

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
