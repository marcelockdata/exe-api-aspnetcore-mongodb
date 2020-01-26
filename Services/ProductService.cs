using System.Collections.Generic;
using System.Threading.Tasks;
using apimongodb.Models;
using MongoDB.Driver;

namespace apimongodb.Services
{
    public class ProductService
    {
    private readonly IMongoCollection<Product> _product;

        public ProductService(IProductDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _product = database.GetCollection<Product>(settings.ProductCollectionName);
        }

        public async Task<List<Product>> Get() 
        {
          var productCollection =  await _product.FindAsync(product => true);
          return await productCollection.ToListAsync();
        }

        public async Task<Product> Get(string id)
        {
           var product = await  _product.FindAsync<Product>(product => product.Id == id);
           return await product.FirstOrDefaultAsync();
        }

        public async Task<Product> Create(Product product)
        {
            await _product.InsertOneAsync(product);
            return product;
        }

        public async void Update(string id, Product productIn) =>
            await _product.ReplaceOneAsync(product => product.Id == id, productIn);

        public async void Remove(Product productIn) =>
           await _product.DeleteOneAsync(product => product.Id == productIn.Id);

        public async void Remove(string id) => 
            await _product.DeleteOneAsync(product => product.Id == id);
    }
}