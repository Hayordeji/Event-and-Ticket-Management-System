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
        private readonly IEventRepository _eventRepo;
        public TicketController(ITicketRepository ticketRepo, IEventRepository  eventRepo)
        {
            _ticketRepo = ticketRepo;
            _eventRepo = eventRepo;
        }

        [HttpPost]
        public async Task<IActionResult> Create ([FromBody] int eventId,string ticketType, decimal price)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var objectEvent = await _eventRepo.GetByIdAsync(eventId); 
            var newTicket = await _ticketRepo.CreateTicketAsync(objectEvent,ticketType,price);
            return Ok(newTicket);
        }

        [HttpGet]
        public async Task<IActionResult> Tickets()
        {
            var tickets = await _ticketRepo.GetTicketsAsync();
            if (tickets == null)
            {
                return NotFound();
            }

            return Ok(tickets);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Ticket([FromRoute]int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ticket = await _ticketRepo.GetTicketById(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return Ok(ticket);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete (int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ticketToDelete = await _ticketRepo.DeleteTicket(id);
            if (ticketToDelete == null)
            {
                return NotFound();
            }
            return Ok(ticketToDelete);
        }
    }
}
