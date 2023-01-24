using Microsoft.AspNetCore.Mvc;

namespace Biplov.Serilog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IgnoreController : ControllerBase
    {
        [HttpGet]
        public IActionResult Ignore() => Ok("Ignored");
    }
}
