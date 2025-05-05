using System.ComponentModel.DataAnnotations;

namespace SmallBizManager.Models
{
    public class OrderItemInputModel
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Unit Price must be greater than 0.")]

        public decimal? UnitPrice { get; set; }

    }
}
