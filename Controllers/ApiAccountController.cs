using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using SmallBizManager.Data;
using SmallBizManager.Models.Auth;
using SmallBizManager.Services;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace SmallBizManager.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ApiAccountController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtTokenService _jwtTokenService;
        public ApiAccountController(ApplicationDbContext context, JwtTokenService jwtTokenService)
        {
            _context = context;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest model)
        {
            var user = _context.Users.SingleOrDefault(u => u.Username == model.Username);
            if (user == null || !VerifyPassword(model.Password, user.PasswordHash, user.PasswordSalt))
                return Unauthorized("Invalid credentials.");

            var token = _jwtTokenService.GenerateToken(user);
            return Ok(new { token });
        }

        private bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(storedHash);
            }
        }


      
    }
}
