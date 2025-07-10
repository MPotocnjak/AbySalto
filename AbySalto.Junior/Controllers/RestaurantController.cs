using AbySalto.Junior.Infrastructure.Database;
using AbySalto.Junior.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AbySalto.Junior.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantController : Controller
    {
        private readonly IApplicationDbContext _context;

        public RestaurantController(IApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("order")]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Osiguraj da se CreatedAt postavi ako nije poslan
            if (order.CreatedAt == default)
                order.CreatedAt = DateTime.UtcNow;

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }

        [HttpGet("order/{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id);

            return order == null ? NotFound() : Ok(order);
        }

        [HttpGet("order/{id}/total")]
        public async Task<ActionResult<decimal>> GetTotalAmount(int id)
        {
            var order = await _context.Orders
                .AsNoTracking()
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return NotFound();

            return Ok(order.TotalAmount);
        }

        [HttpGet("allOrders")]
        public async Task<ActionResult<List<Order>>> GetAllOrders()
        {
            var orders = await _context.Orders
                .Include(o => o.Items)
                .AsNoTracking()
                .ToListAsync();

            return Ok(orders);
        }

        [HttpGet("allOrders/sortedByTotal")]
        public async Task<ActionResult<List<Order>>> GetOrdersSortedByTotal()
        {
            var orders = await _context.Orders
                .Include(o => o.Items)
                .AsNoTracking()
                .ToListAsync();

            var sorted = orders.OrderByDescending(o => o.TotalAmount).ToList();
            return Ok(sorted);

        }

    }
}
