using System.ComponentModel.DataAnnotations;

namespace SmallBizManager.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public ICollection<Order>? Orders { get; set; }

        public string Address { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive value.")]

        public decimal Salary { get; set; }

    }

}
