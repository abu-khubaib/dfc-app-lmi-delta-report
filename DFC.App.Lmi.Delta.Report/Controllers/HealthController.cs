using Microsoft.AspNetCore.Mvc;

namespace DFC.App.Lmi.Delta.Report.Controllers
{
    public class HealthController : Controller
    {
        [HttpGet]
        [Route("health/ping")]
        public IActionResult Ping()
        {
            return Ok();
        }
    }
}
