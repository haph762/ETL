using ETL.Application.Products.EventHandlers;

namespace ETL.Application.Common.RabbitMQ
{
    public class RabbitMQConsumer : BackgroundService
    {
        private readonly RabbitMQProductReceive _rabbitMQProductReceive;

        public RabbitMQConsumer(RabbitMQProductReceive rabbitMQProductReceive)
        {
            _rabbitMQProductReceive = rabbitMQProductReceive;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Danh sách các message muốn nhận từ RabbitMQ thêm tại đây
            await _rabbitMQProductReceive.ReceiveMessage();
        }
    }
}
