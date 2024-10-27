using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TicketPurchaseAPI.Dto.EventDto;
using TicketPurchaseAPI.Extensions;
using TicketPurchaseAPI.Interface;
using TicketPurchaseAPI.Mapper;
using TicketPurchaseAPI.Model;

namespace TicketPurchaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _eventRepository;
        private readonly UserManager<AppUser> _userManager;

        public EventController(IEventRepository eventRepository, UserManager<AppUser> userManager)
        {
            _eventRepository = eventRepository;
            _userManager = userManager;
        }

        //Action method to create event
        [HttpPost("/event/create")]
        [Authorize]
        public async Task<IActionResult> CreateEvent([FromBody] EventCreateDto eventModel)
        {
            //check input
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //fetch the logged in username
            var user = User.GetUsername();
            var newEventDto = eventModel.ToEventCreateDto();
            
            //add new event in the DB
            var newEvent = await _eventRepository.Create(newEventDto, user);
            
            if (newEvent == null)
            {
                return StatusCode(400, "Event is empty");
            }

            return Ok(newEvent);
        }

        //Action method to Get List of events
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetEvents()
        {
            var events = await _eventRepository.GetAsync();
            
            
            if (events == null)
            {
                return StatusCode(500, "There are no events");
            }
            return Ok(events);
        }

        //Action method to get a particular event
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            //check input
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //fetch the event in the DB
            var eventRequested = await _eventRepository.GetByIdAsync(id);
            if (eventRequested == null)
            {
                return NotFound();
            }

            return Ok(eventRequested);
        }

        //Action method to Upate a particular event informantion
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] EventUpdateDto updateDto)
        {
            //check input
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            //fetch the event in the DB
            var eventToUpdate = await _eventRepository.GetByIdAsync(id);
            if (eventToUpdate == null)
            {
                return NotFound();
            }
            var updatedEvent = updateDto.ToEventUpdateDto();

            //update the event
            await _eventRepository.Update(updatedEvent, id);

            return Ok(updatedEvent);

        }

        //Action method to delete a particular event
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            //fetch the event in the DB
            var eventToDelete = await _eventRepository.GetByIdAsync(id);
            if (eventToDelete == null)
            {
                return NotFound();
            }
            //delete event
            await _eventRepository.Delete(id);
            return Ok(eventToDelete);
        }
    }
}
