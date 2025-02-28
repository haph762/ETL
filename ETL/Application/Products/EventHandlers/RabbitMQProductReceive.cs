using ETL.Application.Common.Helper;
using ETL.Domain;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace ETL.Application.Products.EventHandlers
{
    public class RabbitMQProductReceive
    {
        private readonly RabbitMQSettings _settings;
        private readonly MongoDBSettings _mongDBSettings;
        private readonly IServiceScopeFactory _scopeFactory;

        public RabbitMQProductReceive(IOptions<RabbitMQSettings> options,
            IOptions<MongoDBSettings> mongDBSettings, IServiceScopeFactory scopeFactory)
        {
            _settings = options.Value;
            _mongDBSettings = mongDBSettings.Value;
            _scopeFactory = scopeFactory;
        }

        public async Task ReceiveMessage()
        {
            var factory = new ConnectionFactory() { HostName = _settings.HostName, Port = _settings.Port };
            var connection = await factory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: _settings.QueueName, durable: false, exclusive: false, autoDelete: false,
                arguments: null);

            var consumer = new AsyncEventingBasicConsumer(channel);
            var product = new Product();
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                product = JsonSerializer.Deserialize<Product>(message);

                // Lưu MongoDB
                using var scope = _scopeFactory.CreateScope();
                var mongoDatabase = scope.ServiceProvider.GetRequiredService<IMongoDatabase>();
                var mongoCollection = mongoDatabase.GetCollection<ProductReadModel>(_mongDBSettings.Collection);
                var mongoProduct = new List<ProductReadModel> {
                        new ProductReadModel {
                            Id = product.Id.ToString(),
                            Name = product.Name,
                            Price = product.Price
                        }
                    };
                await mongoCollection.InsertManyAsync(mongoProduct);
            };

            await channel.BasicConsumeAsync(_settings.QueueName, autoAck: true, consumer: consumer);
        }
    }
}
