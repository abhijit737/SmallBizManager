using SmallBizManager.Models.Auth;
using System.Threading.Tasks;

namespace SmallBizManager.Services
{
    public interface IAuthService
    {
       public Task<bool> LoginAsync(LoginRequest model);
      public  Task<bool> RegisterAsync(RegisterRequest model);
      public  void Logout();
    }
}
