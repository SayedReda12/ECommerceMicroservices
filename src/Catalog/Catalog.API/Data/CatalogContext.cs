using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext:ICatalogContext
    {
        private readonly IConfiguration _configuration;

        public CatalogContext(IConfiguration configuration)
        {
            _configuration = configuration;
            var client = new MongoClient(configuration["DatabaseStrings:ConnectionString"]);
            var database = client.GetDatabase(configuration["DatabaseStrings:DatabaseName"]);

            Products = database.GetCollection<Product>(nameof(Product));
        }
        public IMongoCollection<Product> Products { get; }
    }
}
