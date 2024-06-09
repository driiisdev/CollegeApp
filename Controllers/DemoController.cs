//using CollegeApp.MyLogging
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CollegeApp.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly ILogger<DemoController> _logger;

        public DemoController(ILogger<DemoController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult Index()
        {
            _logger.LogTrace("Log message from trace method");
            _logger.LogDebug("Log message from trace method");
            _logger.LogInformation("Log message from trace method");
            _logger.LogWarning("Log message from trace method");
            _logger.LogError("Log message from trace method");

            return Ok();
        }
    }
}
