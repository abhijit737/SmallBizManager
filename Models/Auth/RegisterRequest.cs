namespace SmallBizManager.Models.Auth
{
    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; } // Admin, Staff
    }

    public enum UserRole
    {
        Admin,
        Staff
    }


}
