using Management.Application.Internal.CommandServices;
using Management.Application.Internal.QueryServices;
using Management.Application.RabbitMq.MessageService;
using Management.Application.RabbitMq.Publishers;
using Management.Domain.Repositories;
using Management.Domain.Service;
using Management.Infrastructure.Persistence.MongoEFC.Repositories;
using Management.Infrastructure.RabbitMQ;
using Management.Shared.Domain.Repositories;
using Management.Shared.Infrastructure.MongoEFC.Persistence;
using Management.Shared.Infrastructure.MongoEFC.Persistence.Repository;
using Management.Shared.Infrastructure.MongoEFC.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();


var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var factory = new ConnectionFactory
{
    Uri = new Uri(configuration["RabbitMQ:Url"]),
    Ssl = new SslOption { Enabled = true },
    DispatchConsumersAsync = true
};

builder.Services.AddSingleton(factory);


builder.Services.AddScoped<IRabbitMqService, RabbitMqService>();
builder.Services.AddScoped<IProductCommandService,ProductCommandService>();
builder.Services.AddScoped<IProductQueryService,ProductQueryService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ProductPublisher>();
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

var connectionString = configuration.GetConnectionString("MongoDb");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (connectionString != null) 
        options.UseMongoDB(connectionString, "inventory");
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();


app.Run();

