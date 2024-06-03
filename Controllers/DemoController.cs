using CollegeApp.MyLogging;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        //1. Strongly coupled/tightly coupled
        private readonly IMyLogger _myLogger;
        public DemoController(IMyLogger myLogger)
        {
            _myLogger = myLogger;
        }

        [HttpGet]
        public ActionResult Index()
        {
            _myLogger.Log("Index method started");
            return Ok();
        }
    }
}
