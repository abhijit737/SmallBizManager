using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace SmallBizManager.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int OrderId { get; set; }


        [JsonIgnore] 
        public Order Order { get; set; }

        [Required]
        public int ProductId { get; set; }

        [BindNever]
        public Product Product { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Unit Price must be greater than 0.")]
        public decimal UnitPrice { get; set; }


        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

    }

}
