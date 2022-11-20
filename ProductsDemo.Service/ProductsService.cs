using AutoMapper;
using DataAccess = ProductsDemo.Data.DataAccess;
using ProductsDemo.Model;
using ProductsDemo.Model.Enums;
using ProductsDemo.Repository.Interface;
using ProductsDemo.Service.Interface;

namespace ProductsDemo.Service
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _productsRepository;
        private IMapper _mapper;
        private static Random random = new Random();
        public ProductsService(IProductsRepository productsRepository, IMapper _mapper)
        {
            this._productsRepository = productsRepository;
            this._mapper = _mapper;
        }
        public ProductResponse GetProductById(Guid productId)
        {
            var response = _productsRepository.GetProductById(productId);
            if (response != null)
            {
                var productResponse = _mapper.Map<ProductResponse>(response);
                foreach (var article in productResponse?.Articles)
                {
                    article.ArticleName = $"{productResponse.ProductName}{"-"}{article.ColourCode}";
                }
                return productResponse;
            }
            return null;
        }
        public async Task<Task> CreateProduct(ProductRequest product)
        {
            // var productEntity = _mapper.Map<Product>(product);
            var productEntity = new DataAccess.Product
            {
                Productcode = GenerateProductCode(product),
                Createddate = DateTime.Now,
                Createdby = "User",
                Productid = new Guid(product.ProductId),
                Productname = product.ProductName,
                Productyear = product.ProductYear,
                Articles = MapArticle(product),
                Sizescaleid = new Guid(product.SizeScaleId),
                Channelid = product.ChannelId                
            };
            return _productsRepository.CreateProduct(productEntity);
        }
        private List<DataAccess.Article> MapArticle(ProductRequest product)
        {
            var articleList = new List<DataAccess.Article>();
            foreach (var productArticle in product.Articles) {
                var article = new DataAccess.Article()
                {
                    Articleid = new Guid(productArticle.ArticleId),
                    Articlename = productArticle.ArticleName,
                    Colorcode = productArticle.ColourCode,
                    Colorname = productArticle.ColourName,
                    Colourid = new Guid(productArticle.ColourId)
                };
                articleList.Add(article);
            }
            return articleList;
        }
        private string GenerateProductCode(ProductRequest product)
        {
            string productCode = string.Empty;
            var lastAddedProduct = _productsRepository.GetLastAddedProduct(product.ChannelId);
            if (product.ChannelId == (int)Channels.Store)
            {
                int lastAdded = lastAddedProduct != null ? Convert.ToInt32(lastAddedProduct.Productcode.Substring(4, lastAddedProduct.Productcode.Length - 4)) : 0;
                productCode = $"{product.ProductYear}{GenerateSequenceIdForChannelStore(lastAdded)}";
            }
            else if (product.ChannelId == (int)Channels.Online)
            {
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                productCode = new string(Enumerable.Repeat(chars, 6)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
                if (productCode != lastAddedProduct?.Productcode) {
                    return productCode;
                } else
                {
                    GenerateProductCode(product);
                }
            }
            else
            {
                int lastAdded = lastAddedProduct != null ? Convert.ToInt32(lastAddedProduct.Productcode) : 10000000;
                productCode = GenerateSequenceIdForChannelAll(lastAdded);
            }
            return productCode;
        }
        private string GenerateSequenceIdForChannelStore(int lastAdded)
        {
            return Convert.ToString(lastAdded + 1).PadLeft(3, '0');
        }
        private string GenerateSequenceIdForChannelAll(int lastAdded)
        {
            return Convert.ToString(lastAdded + 1);
        }
    }
}