using Actio.Common.Commands;
using Actio.Services.Identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Actio.Services.Identity.Controllers
{
    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountController : Controller
    {
        private readonly IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] AuthenticateUser command)
            => Json(await userService.LoginAsync(command.Email, command.Password));
    }
}