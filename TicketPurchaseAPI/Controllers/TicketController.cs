using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TicketPurchaseAPI.Interface;
using TicketPurchaseAPI.Model;
using TicketPurchaseAPI.Services;
using static TicketPurchaseAPI.Model.Ticket;

namespace TicketPurchaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketRepository _ticketRepo;
        private readonly IEventRepository _eventRepo;
        private readonly IQRGeneratorService _qrGeneratorService;
        public TicketController(ITicketRepository ticketRepo, IEventRepository  eventRepo, IQRGeneratorService qRGeneratorService)
        {
            _ticketRepo = ticketRepo;
            _eventRepo = eventRepo;
            _qrGeneratorService = qRGeneratorService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create (int eventId,string ticketType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var objectEvent = await _eventRepo.GetByIdAsync(eventId);
            if (objectEvent == null)
            {
                return StatusCode(500, "Event not found");
            }
            var newTicket = await _ticketRepo.CreateTicketAsync(objectEvent,ticketType);
            await _qrGeneratorService.GenerateImage(newTicket);
            return Ok(newTicket);
        }

        [HttpPost("{id}/qrcodegen")]
        public async Task<IActionResult> QRCodeData([FromRoute]int id)
        {
            var ticket = await _ticketRepo.GetTicketById(id);
            await _qrGeneratorService.GenerateImage(ticket);

            return Ok();
        }

        [HttpGet("qrcode/validate")]
        public async Task<IActionResult> Validate(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _ticketRepo.TicketExists(id))
            {
                return Ok("Ticket is Validated");
            }
            return NotFound("Ticket was not found");
        }


        [HttpGet]
        public async Task<IActionResult> Tickets()
        {
            var tickets = await _ticketRepo.GetTicketsAsync();
            if (Ticket == null)
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
