using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TicketPurchaseAPI.Extensions;
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

        [HttpPost("Create")]
        [Authorize]
        public async Task<IActionResult> Create (int eventId,string ticketType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var user = User.GetUsername();
            var eventObject = await _eventRepo.GetByIdAsync(eventId);
            if (eventObject == null)
            {
                return StatusCode(500, "Event not found");
            }
            else if (eventObject.TicketSold == eventObject.Capacity)
            {
                return BadRequest("Event has been sold out");
            }
           
            var newTicket = await _ticketRepo.CreateTicketAsync(eventObject, ticketType,user);
            return Ok(newTicket);
            
        }

        [HttpPost("{id}/QRrCodeGen")]
        [Authorize]
        public async Task<IActionResult> QRCodeData([FromRoute]int id)
        {
            var ticket = await _ticketRepo.GetTicketById(id);
            if (ticket.Status == TicketStatus.Pending)
            {
                return BadRequest("Can't Generate QRCode...Make Payment first"); 
            }
            await _qrGeneratorService.GenerateImage(ticket);

            return Ok();
        }

        [HttpGet("qrcode/Validate/{id}")]
        [Authorize]
        public async Task<IActionResult> Validate(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           
            var ticket = await _ticketRepo.GetTicketById(id);

            switch (ticket.Status)
            {
                case TicketStatus.Pending:
                    return BadRequest("Make payment for ticket first");
                case TicketStatus.Validated:
                    return BadRequest("Ticket has already been validated before");
                case TicketStatus.Paid:
                    await _ticketRepo.VaidateTicket(id);
                    return Ok("Ticket is Validated" + ticket);
            }

            return BadRequest("Something went wrong");   
        }

        [HttpGet("/Confirmpayment/{id}")]
        public async Task<IActionResult> ConfirmPayment(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            

            if (await _ticketRepo.TicketExists(id))
            {
                var ticket = await _ticketRepo.ConfirmPayment(id);
                return Ok("Payment Confirmed...." + "Ticket Detail : " + ticket);
            }
            
            return NotFound("Ticket was not found");
        }

        [HttpGet]
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> TicketById([FromRoute]int id)
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
        [Authorize]
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
