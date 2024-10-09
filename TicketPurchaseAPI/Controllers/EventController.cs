using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketPurchaseAPI.Interface;
using TicketPurchaseAPI.Model;

namespace TicketPurchaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _eventRepository;
        public EventController(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }


        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] Event eventModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newEvent = await _eventRepository.Create(eventModel);
            if (newEvent == null)
            {
                return StatusCode(400, "Event is empty");
            }

            return Ok(newEvent);
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            var events = await _eventRepository.GetAsync();
            if (events == null)
            {
                return StatusCode(400, "There are no events");
            }
            return Ok(events);
        }
    }
}
