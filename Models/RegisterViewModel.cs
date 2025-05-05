using SmallBizManager.Models.Auth;
using System.ComponentModel.DataAnnotations;

namespace SmallBizManager.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]

        public string Password { get; set; }
        [Required]

        public UserRole Role { get; set; }
    }

}
