using ETL.Application.Common.Helper;
using ETL.Application.Common.RabbitMQ;
using ETL.Application.Products.EventHandlers;
using ETL.Infrastructure.DataSqlServer;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static void AddWebServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

        builder.Services.AddSingleton<IMongoClient>(sp => new MongoClient(builder.Configuration["MongoDb:ConnectionString"]));

        builder.Services.AddScoped<IMongoDatabase>(sp =>
            sp.GetRequiredService<IMongoClient>().GetDatabase(builder.Configuration["MongoDb:Collection"]));

        builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQ"));

        builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDb"));

        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        builder.Services.AddSingleton<RabbitMQProductSend>();

        builder.Services.AddSingleton<RabbitMQProductReceive>();

        builder.Services.AddHostedService<RabbitMQConsumer>();

        builder.Services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen();
    }
}