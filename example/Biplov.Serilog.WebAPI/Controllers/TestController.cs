using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace Biplov.Serilog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ILogger _logger;

        public TestController(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public IActionResult Hello()
        {
            _logger.Information("This is a info log for hello world");
            _logger.Error("This is an error log for hello world");
            return Ok("hello world");

        } 
    }
}
