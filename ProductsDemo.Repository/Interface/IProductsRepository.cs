using ProductsDemo.Data.DataAccess;

namespace ProductsDemo.Repository.Interface
{
    public interface IProductsRepository
    {
        Product GetProductById(Guid productId);
        Task<int> CreateProduct(Product product);
        Product GetLastAddedProduct(int channelId);
    }
}
