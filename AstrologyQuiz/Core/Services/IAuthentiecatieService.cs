using Core.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IAuthentiecatieService
    {
        Task<BearerToken> GenerateJWT(string name);
        Task<bool> Login(Login login);
    }
}