using AbySalto.Junior.Infrastructure.Database;
using AbySalto.Junior.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AbySalto.Junior.Controllers
{
    /// <summary>
    /// API kontroler za upravljanje narudzbama restorana
    /// Omogucuje kreiranje, dohvat i izmjenu narudzbi
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantController : Controller
    {
        private readonly IApplicationDbContext _context;

        public RestaurantController(IApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Kreira novu narudzbu i sprema je u bazu
        /// </summary>
        /// <param name="order">Podaci o narudzbi koju treba dodati</param>
        /// <returns>HTTP 201 sa kreiranom narudzbom ili 400 ako model nije ispravan</returns>
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

        /// <summary>
        /// Dohvaca jednu narudzbu prema njezinom ID-u
        /// </summary>
        /// <param name="id">Identifikator narudzbe</param>
        /// <returns>HTTP 200 s detaljima narudzbe ili 404 ako nije pronadjena</returns>
        [HttpGet("order/{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id);

            return order == null ? NotFound() : Ok(order);
        }

        /// <summary>
        /// Racuna ukupan iznos narudzbe prema ID-u
        /// </summary>
        /// <param name="id">Identifikator narudzbe</param>
        /// <returns>Decimalna vrijednost ukupnog iznosa ili HTTP 404 ako narudzba ne postoji</returns>
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

        /// <summary>
        /// Dohvaca sve narudzbe u sustavu
        /// </summary>
        /// <returns>Lista svih narudzbi sa njihovim stavkama</returns>
        [HttpGet("allOrders")]
        public async Task<ActionResult<List<Order>>> GetAllOrders()
        {
            var orders = await _context.Orders
                .Include(o => o.Items)
                .AsNoTracking()
                .ToListAsync();

            return Ok(orders);
        }

        /// <summary>
        /// Dohvaca sve narudzbe sortirane silazno prema ukupnom iznosu
        /// </summary>
        /// <returns>Sortirana lista narudzbi</returns>
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

        /// <summary>
        /// Azurira status postojece narudzbe
        /// </summary>
        /// <param name="id">ID narudzbe kojoj mijenjamo status</param>
        /// <param name="newStatus">Novi status kao string (NaCekanju, UPripremi ili Zavrsena)</param>
        /// <returns>Poruka o uspjesnoj izmjeni ili greska ako status nije ispravan</returns>
        [HttpPatch("order/{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] string newStatus)
        {
            if (!Enum.TryParse<OrderStatus>(newStatus, out var status))
                return BadRequest("Nevažeći status.");

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
                return NotFound();

            order.Status = status;
            await _context.SaveChangesAsync();

            var message = $"Status narudžbe #{order.Id} je uspješno promijenjen na '{newStatus}'.";

            return Ok(message);
        }

    }
}
