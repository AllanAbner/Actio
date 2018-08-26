using Actio.Common.Auth;
using System.Threading.Tasks;

namespace Actio.Services.Identity.Services
{
    public interface IUserService
    {
        Task RegisterAsync(string email, string password, string name);

        Task<JsonWebToken> LoginAsync(string email, string password);
    }
}