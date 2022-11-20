using ProductsDemo.Model;

namespace ProductsDemo.Service.Interface
{
    public interface IProductsService
    {
        ProductResponse GetProductById(Guid productId);
        Task<Task> CreateProduct(ProductRequest product);
    }
}
