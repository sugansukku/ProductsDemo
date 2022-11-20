using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using ProductsDemo.Model;
using System.Net;
using System.Net.Http.Headers;

namespace ProductsDemo.Api.Test
{
    [TestClass]
    public class ProductsApiTests
    {
        private HttpClient _httpClient;
        public ProductsApiTests()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateDefaultClient();
        }

        [TestMethod]
        public void CreateProduct_ValidationError()
        {
            ProductRequest productRequest = new ProductRequest()
            {
            };
            var response = PostAsync(productRequest);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [TestMethod]
        public void CreateProduct_Success()
        {
            ProductRequest productRequest = new ProductRequest()
            {
                ProductId = "67d219d7-4b03-4d60-9214-d4b6ab1c4cbf",
                ProductName = "From Test",
                ProductYear = 2022,
                ChannelId = 1,
                SizeScaleId = "2faa12ba-5d73-4d8f-a736-0e0ba6769a55",
                Articles = new List<Article>
                     {
                       new Article {
                             ColourId =  "c01941ef-08e6-479e-ae19-ff95d770022c",
                             ColourName =  "string",
                             ColourCode =  "string",
                             ArticleId =  "92c4521c-b699-4d9c-a36c-f66764d5e83c",
                             ArticleName = "string"
                       }
                }
            };
            var response = PostAsync(productRequest);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void GetProductById_Success()
        {
            var productId = new Guid("67d219d7-4b03-4d60-9214-d4b6ab1c4cbf");
            var response = GetProductByIdAsync(productId);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void GetProductById_NotFound()
        {
            Guid productId = Guid.NewGuid();
            var response = GetProductByIdAsync(productId);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        private HttpResponseMessage PostAsync(ProductRequest productRequest)
        {
            var myContent = JsonConvert.SerializeObject(productRequest);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = _httpClient.PostAsync("/api/Products/CreateProduct", byteContent).Result;
            var data = response.Content.ReadAsStringAsync();
            return response;
        }

        private HttpResponseMessage GetProductByIdAsync(Guid productId)
        {
            var response = _httpClient.GetAsync($"{"/api/Products/GetProductById?productId"}{productId}").Result;
            return response;
        }
    }
}