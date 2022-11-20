namespace ProductsDemo.Model
{
    public class ProductResponse
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public int ChannelId { get; set; }
        public Guid SizeScaleId { get; set; }
        public List<Article> Articles { get; set; }
        public List<Size> Sizes { get; set; }

    }
}
