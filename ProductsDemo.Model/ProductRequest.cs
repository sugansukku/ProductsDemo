using System.ComponentModel.DataAnnotations;

namespace ProductsDemo.Model
{
    public class ProductRequest
    {
        [Required] //, MaxLength(100)
        public string ProductId { get; set; }

        [Required, MinLength(3)] //MaxLength(100)
        public string ProductName { get; set; }
        [Required]
        public int ProductYear { get; set; }
        [Required]
        public int ChannelId { get; set; }
        public string SizeScaleId { get; set; }
        public List<Article> Articles { get; set; }

    }
}