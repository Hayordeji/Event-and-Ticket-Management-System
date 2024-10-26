﻿using Microsoft.AspNetCore.Authorization;
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

        
        [HttpPost("/event/create")]
        [Authorize]
        public async Task<IActionResult> CreateEvent([FromBody] EventCreateDto eventModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = User.GetUsername();
            var newEventDto = eventModel.ToEventCreateDto();
            var newEvent = await _eventRepository.Create(newEventDto, user);
            
            if (newEvent == null)
            {
                return StatusCode(400, "Event is empty");
            }

            return Ok(newEvent);
        }


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

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var eventRequested = await _eventRepository.GetByIdAsync(id);
            if (eventRequested == null)
            {
                return NotFound();
            }

            return Ok(eventRequested);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] EventUpdateDto updateDto)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            var eventToUpdate = await _eventRepository.GetByIdAsync(id);
            if (eventToUpdate == null)
            {
                return NotFound();
            }
            var updatedEvent = updateDto.ToEventUpdateDto();
            await _eventRepository.Update(updatedEvent, id);

            return Ok(updatedEvent);

        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
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
