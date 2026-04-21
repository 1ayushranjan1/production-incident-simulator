using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductionIncidentSimulator.API.Services;

namespace ProductionIncidentSimulator.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrdersController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("create")]
        public IActionResult CreateOrder([FromBody] CreateOrderRequest request)
        {
            var order = _orderService.CreateOrder(request);
            return Ok(order);
        }
        [HttpPost("create-with-keys")]
        public IActionResult CreateOrderWithKeys([FromBody] CreateOrderRequest request)
        {
            var order = _orderService.CreateOrderWithKeys(request);
            return Ok(order);
        }

        [HttpGet("getorder")]
        public IActionResult GetOrders()
        {
            return Ok(_orderService.GetOrders());
        }

        [HttpPost("retry/{id}")]
        public IActionResult Retry(int id)
        {
            try
            {
                _orderService.RetryIncident(id);
                return Ok("Retry processed");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
