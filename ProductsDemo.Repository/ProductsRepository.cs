using Microsoft.EntityFrameworkCore;
using ProductsDemo.Data.DataAccess;
using ProductsDemo.Model;
using ProductsDemo.Repository.Interface;

namespace ProductsDemo.Repository
{
    public class ProductsRepository: IProductsRepository
    {
        private ProductsContext _productsDBContext;
        public ProductsRepository(ProductsContext productsDBContext)
        {
            _productsDBContext = productsDBContext;
        }
        public Product GetProductById(Guid productId)
        {
            return this._productsDBContext.Products.Include(p => p.Articles)
                .Where(p => p.Productid == productId).FirstOrDefault();
        }
        public async Task<int> CreateProduct(Product product)
        {
            _productsDBContext.Attach(product);
            _productsDBContext.Products.Add(product);
            foreach(var article in product.Articles)
            {
                _productsDBContext.Articles.Add(article);
                _productsDBContext.Entry(article).State = EntityState.Added;
            }
            _productsDBContext.Entry(product).State = EntityState.Added;
            var res = await _productsDBContext.SaveChangesAsync();
            return res;
        }
        public Product GetLastAddedProduct(int channelId)
        {
            return this._productsDBContext.Products.OrderByDescending(p => p.Createddate)
                .Where(p => p.Channelid == channelId).FirstOrDefault();
        }

    }
}