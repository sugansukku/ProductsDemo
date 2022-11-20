using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Identity.Web.Resource;
using ProductsDemo.Api.Hubs;
using ProductsDemo.Logging.Interface;
using ProductsDemo.Model;
using ProductsDemo.Service.Interface;
using StackExchange.Redis;
using System.Net.Http.Headers;

namespace ProductsDemo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class ProductsController : ControllerBase
    {

        private readonly ILog _logger;
        private readonly IProductsService _productsService;
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly IConfiguration _configuration;
        private readonly IDatabase _redisCacheConnection;

        public ProductsController(ILog logger, IProductsService productsService, IHubContext<NotificationHub> hubContext, IConfiguration configuration)
        {
            _logger = logger;
            _productsService = productsService;
            _hubContext = hubContext;
            _configuration = configuration;
            //_redisCacheConnection = new Lazy<ConnectionMultiplexer>(() =>
            //{
            //    string cacheConnectionString = _configuration["Redis:ConnectionString"];
            //    return ConnectionMultiplexer.Connect(cacheConnectionString);

            //}).Value.GetDatabase(); // For Redis Cache
        }

        /// <summary>
        /// Returns a product matching the given product id.
        /// </summary>
        /// <param name="productId">The unique identifier of the product</param>
        /// <returns>A matching product</returns>

        // [Authorize(Roles = "Manager")]
        [HttpGet("GetProductById/{productId:Guid}")]
        public IActionResult GetProductById(Guid productId)
        {
            try
            {
                _logger.Information("Getting product details by product id");
                var product = _productsService.GetProductById(productId);
                if (product == null)
                {
                    return NotFound("Invalid product id");
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.Error($"Exception occured {ex}");
                return StatusCode(500, "Error occured while fetching the product");
            }
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="product">Product details</param>

        [HttpPost]
        [Route("CreateProduct")]
        public async Task<ActionResult> CreateProductAsync(ProductRequest product)
        {
            try
            {
                if (product == null || !ModelState.IsValid)
                {
                    return BadRequest("Please provide valid product");
                }
                if (product.ProductName.Length > 100)
                {
                    return StatusCode(406, "ProductName has max 100 character length limitation. Please provide valid ProductName");
                }
                _logger.Information("Creating product");

                var colors = CallColorCodeApi();
                foreach (var article in product.Articles)
                {
                    var color = colors.Where(c => c.ColourId == article.ColourId).FirstOrDefault();
                    article.ColourName = color.ColourName;
                    article.ColourCode = color.ColourCode;

                }
                var createdProduct = await _productsService.CreateProduct(product);
                await _hubContext.Clients.All.SendAsync("createproduct", "Product created successfully");
                return StatusCode(201, "Product created successfully");
            }
            catch (Exception ex)
            {
                _logger.Error($"Exception occured {ex}");
                return StatusCode(500, "Error while creating a product");
            }
        }
        private IEnumerable<Color> CallColorCodeApi()
        {
            // For Redis Cache
            //if (_redisCacheConnection.StringGet("cachekey").IsNullOrEmpty)
            //{
            //}
            //else
            //{
            //    _redisCacheConnection.StringSet("cachekey", "data from colors api", TimeSpan.FromMinutes(30));
            //}

            string url = _configuration["ApiUrl:Colors"];
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    var colors = response.Content.ReadFromJsonAsync<IEnumerable<Color>>().Result;
                    return colors;
                }
                return null;
            }
        }
    }
}