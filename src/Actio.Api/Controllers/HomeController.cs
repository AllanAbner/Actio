using Microsoft.AspNetCore.Mvc;

namespace Actio.Api.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Get() => Content("Hello from action Api");
       
    }
}