using Newtonsoft.Json;

namespace SmallBizManager.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
      public string Description { get; set; }
   //   public string ImageUrl { get; set; }
        public decimal Stock { get; set; }

        [JsonIgnore]
        public ICollection<OrderItem> ? OrderItems { get; set; }

        public string? ImagePath { get; set; }
        public string? ThumbnailPath { get; set; }

        
    }

}
