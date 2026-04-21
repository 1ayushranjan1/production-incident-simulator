using Microsoft.AspNetCore.Mvc;

namespace ProductionIncidentSimulator.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetStats()
        {
            var totalOrders = _context.Orders.Count();
            var failed = _context.IncidentLogs.Count(x => x.Status == "Failed");
            var resolved = _context.IncidentLogs.Count(x => x.Status == "Resolved");

            return Ok(new
            {
                totalOrders,
                failed,
                resolved
            });
        }
    }
}
