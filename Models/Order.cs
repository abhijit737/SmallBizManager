using System.ComponentModel.DataAnnotations;

namespace SmallBizManager.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]

        public string CustomerName { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public OrderStatus Status { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Total Amount must be a positive value.")]

        public decimal TotalAmount { get; set; }

        public ICollection<OrderItem> Items { get; set; }

        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }


        public enum OrderStatus
        {
            Pending,
            Shipped,
            Completed
        }


    }

}
