using System.ComponentModel.DataAnnotations;

namespace SmallBizManager.Models
{
    public class CreateOrderViewModel
    {
        public string? Id { get; set; }

        [Required]
        public string CustomerName { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public string Status { get; set; }

        public decimal TotalAmount { get; set; }

        [Required]
        public List<OrderItemInputModel> Items { get; set; } 

    }
}
