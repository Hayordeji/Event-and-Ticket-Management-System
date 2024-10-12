using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketPurchaseAPI.Interface;

namespace TicketPurchaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketRepository _ticketRepo;
        public TicketController(ITicketRepository ticketRepo)
        {
            _ticketRepo = ticketRepo;
        }

        [HttpPost]
        public async Task<IActionResult> Create ([FromRoute] int eventId, [FromBody] decimal price, [FromBody] string ticketType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var 
        }
    }
}
