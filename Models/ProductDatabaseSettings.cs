namespace apimongodb.Models
{
    public class ProductDatabaseSettings: IProductDatabaseSettings
    {
        public string ProductCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}