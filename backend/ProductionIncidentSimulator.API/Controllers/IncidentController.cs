using Microsoft.AspNetCore.Mvc;

namespace ProductionIncidentSimulator.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IncidentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public IncidentsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetIncidents()
        {
            var data = _context.IncidentLogs.ToList();
            return Ok(data);
        }
    }
}
