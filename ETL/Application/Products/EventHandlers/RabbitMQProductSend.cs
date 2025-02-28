using ETL.Application.Common.Helper;
using ETL.Domain;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace ETL.Application.Products.EventHandlers
{
    public class RabbitMQProductSend
    {
        private readonly RabbitMQSettings _settings;

        public RabbitMQProductSend(IOptions<RabbitMQSettings> options)
        {
            _settings = options.Value;
        }

        public async Task SendMessage(Product product)
        {
            var factory = new ConnectionFactory() { HostName = _settings.HostName, Port = _settings.Port };
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: _settings.QueueName, durable: false, exclusive: false, autoDelete: false,
                arguments: null);

            var message = JsonSerializer.Serialize(product);
            var body = Encoding.UTF8.GetBytes(message);
            await channel.BasicPublishAsync(exchange: string.Empty, routingKey: _settings.QueueName, body: body);
        }
    }
}
