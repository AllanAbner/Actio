using System.Threading.Tasks;
using Actio.Common.Commands;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;

namespace Actio.Api.Controllers
{
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IBusClient busClient;

        public UsersController(IBusClient busClient)
        {
            this.busClient = busClient ??
                throw new System.ArgumentNullException(nameof(busClient));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Post([FromBody] CreateUser command)
        {
            await busClient.PublishAsync(command);

            return Accepted();
        }
    }
}