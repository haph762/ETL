using ETL.Application.Products.EventHandlers;
using ETL.Domain;
using ETL.Infrastructure.DataSqlServer;
using MediatR;

namespace ETL.Application.Products.Commands
{
    public record CreateProductCommand(string Name, decimal Price) : IRequest<int>;

    public class CreateProductHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly AppDbContext _dbContext;
        private readonly RabbitMQProductSend _messageSender;
        public CreateProductHandler(AppDbContext dbContext, RabbitMQProductSend messageSender)
        {
            _dbContext = dbContext;
            _messageSender = messageSender;
        }
        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product { Name = request.Name, Price = request.Price };
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            await _messageSender.SendMessage(product);
            return product.Id;
        }
    }
}
