using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly IPaymentRepository _paymentRepo;
        private readonly UserManager<AppUser> _userManager;
       
        public TicketController(ITicketRepository ticketRepo, IEventRepository  eventRepo, IQRGeneratorService qRGeneratorService,IPaymentRepository paymentRepo
            ,UserManager<AppUser> userManager)
        {
            _ticketRepo = ticketRepo;
            _eventRepo = eventRepo;
            _qrGeneratorService = qRGeneratorService;
            _paymentRepo = paymentRepo;
            _userManager = userManager;
           
        }

        //Action method to create ticket
        [HttpPost("Create")]
        [Authorize]
        public async Task<IActionResult> Create (int eventId,string ticketType)
        {
            //check input
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //fetch the logged in username
            var user = User.GetUsername();

            //fetch the event in the DB
            var eventObject = await _eventRepo.GetByIdAsync(eventId);
            if (eventObject == null)
            {
                return StatusCode(500, "Event not found");
            }
            //check if event is sold out 
            else if (eventObject.TicketSold == eventObject.Capacity)
            {
                return BadRequest("Event has been sold out");
            }
           
            //Create new ticket
            var newTicket = await _ticketRepo.CreateTicketAsync(eventObject, ticketType,user);

            //CReate new payment record
            var newPayment = await _paymentRepo.CreatePaymentAsync(user, newTicket.Id, newTicket.Price);
            return Ok(newTicket);
            
        }

        //Action method to generate QRCode for a ticket bought
        [HttpPost("{id}/QRrCodeGen")]
        [Authorize]
        public async Task<IActionResult> QRCodeData([FromRoute]int id)
        {
            //Fetch Ticket
            var ticket = await _ticketRepo.GetTicketById(id);

            //Check if ticket has been paid for
            if (ticket.Status == TicketStatus.Pending)
            {
                return BadRequest("Can't Generate QRCode...Make Payment first"); 
            }

            //Generate QRCode
            await _qrGeneratorService.GenerateImage(ticket);

            return Ok();
        }

        //Action method to vaidate a particular ticket
        [HttpGet("qrcode/Validate/{id}")]
        [Authorize]
        public async Task<IActionResult> Validate(int id)
        {
            //check input
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //fetch the ticket in the DB           
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

        //Action method to confirm payment of ticket
        [HttpGet("/Confirmpayment/{id}")]
        public async Task<IActionResult> ConfirmPayment(int id)
        {
            //check input
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            //CHeck if ticket exists
            if (await _ticketRepo.TicketExists(id))
            {
                var ticket = await _ticketRepo.ConfirmPayment(id);
                return Ok("Payment Confirmed...." + "Ticket Detail : " + ticket);
            }
            
            return NotFound("Ticket was not found");
        }


        //Action method to get list of tickets 
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Tickets()
        {
            //fetch the tickets in the DB
            var tickets = await _ticketRepo.GetTicketsAsync();
            if (tickets == null)
            {
                return NotFound();
            }
           
            return Ok(tickets);
        }


        //Action method to get a particular ticket
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> TicketById([FromRoute]int id)
        {
            //check input
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //fetch the ticket in the DB
            var ticket = await _ticketRepo.GetTicketById(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return Ok(ticket);
        }

        //Action method to delete a particular ticket
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete (int id)
        {
            //check input
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Delete the ticket in the DB
            var ticketToDelete = await _ticketRepo.DeleteTicket(id);
            if (ticketToDelete == null)
            {
                return NotFound();
            }
            return Ok(ticketToDelete);
        }

       
    }
}
