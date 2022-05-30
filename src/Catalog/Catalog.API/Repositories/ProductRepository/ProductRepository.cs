using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Repositories.ProductRepository
{
    public class ProductRepository:IProductRepository
    {
        private readonly ICatalogContext _catalogContext;
        public ProductRepository(ICatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _catalogContext.Products.Find(a => true).ToListAsync();
        }

        public async Task<Product> GetProductById(string id)
        {
            return await _catalogContext.Products.Find(a => a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            var filter = Builders<Product>.Filter.Eq(a => a.Name, name);
            return await _catalogContext.Products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategory(string categoryName)
        {
            var filter = Builders<Product>.Filter.Eq(a => a.Category, categoryName);

            return await _catalogContext.Products.Find(filter).ToListAsync();
        }

        public async Task<Product> CreateProduct(Product product)
        {
           await _catalogContext.Products.InsertOneAsync(product);

           return product;
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var updateResult = await _catalogContext.Products.ReplaceOneAsync(a => a.Id == product.Id, replacement: product);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteProduct(string productId)
        {
            var deletedResult = await _catalogContext.Products.DeleteOneAsync(a => a.Id == productId);
            return deletedResult.IsAcknowledged && deletedResult.DeletedCount > 0;
        }
    }
}
