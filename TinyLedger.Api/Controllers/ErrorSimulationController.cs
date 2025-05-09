#if DEBUG
using Microsoft.AspNetCore.Mvc;

namespace TinyLedger.Api.Controllers
{
    [ApiController]
    [Route("test/error")]
    public class ErrorSimulationController : ControllerBase
    {
        [HttpGet("unhandled")]
        public IActionResult TriggerUnhandledException()
        {
            throw new Exception("Boom! Simulated unhandled error.");
        }
    }
}
#endif