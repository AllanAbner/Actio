using System;
using System.Threading.Tasks;
using Actio.Common.Commands;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;

namespace Actio.Api.Controllers
{
    [Route("[controller]")]
    public class ActivitiesController : Controller
    {
        private readonly IBusClient busClient;

        public ActivitiesController(IBusClient busClient)
        {
            this.busClient = busClient ?? throw new System.ArgumentNullException(nameof(busClient));
        }
       
        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]CreateActivity command)
        {
            command.Id = Guid.NewGuid();
          
            command.CreatedAt = DateTime.UtcNow;
            await busClient.PublishAsync(command);

            return Accepted($"activities/{command.Id}");
        }
    }
}