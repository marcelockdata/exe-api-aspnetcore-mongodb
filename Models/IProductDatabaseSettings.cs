

namespace apimongodb.Models
{
    public interface IProductDatabaseSettings 
    {
        string ProductCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}