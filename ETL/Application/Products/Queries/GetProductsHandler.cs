using ETL.Application.Common.Helper;
using ETL.Domain;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ETL.Application.Products.Queries
{
    public record GetProductsQuery() : IRequest<List<ProductReadModel>>;

    public class GetProductsHandler : IRequestHandler<GetProductsQuery, List<ProductReadModel>>
    {
        private readonly IMongoCollection<ProductReadModel> _mongoCollection;
        private readonly MongoDBSettings _mongDBSettings;
        public GetProductsHandler(IMongoDatabase database, IOptions<MongoDBSettings> mongDBSettings)
        {
            _mongDBSettings = mongDBSettings.Value;
            _mongoCollection = database.GetCollection<ProductReadModel>(_mongDBSettings.Collection);
        }
        public async Task<List<ProductReadModel>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            return await _mongoCollection.Find(_ => true).ToListAsync();
        }
    }
}
