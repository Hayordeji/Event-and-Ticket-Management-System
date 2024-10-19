using Microsoft.EntityFrameworkCore;
using TicketPurchaseAPI.Data;
using TicketPurchaseAPI.Dto;
using TicketPurchaseAPI.Interface;
using TicketPurchaseAPI.Model;

namespace TicketPurchaseAPI.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDbContext _context;
        public EventRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Event> Create(Event newEvent)
        {
            await _context.Events.AddAsync(newEvent);
            await _context.SaveChangesAsync();
            return newEvent;
        }

        public async Task<Event> Delete(int id)
        {
            var eventToDelete = await _context.Events.FindAsync(id);
            if (eventToDelete == null)
            {
                return null;
            }

           _context.Remove(eventToDelete);
           await _context.SaveChangesAsync();
           return eventToDelete;
        }

        public async Task<List<Event>> GetAsync()
        {
            var events = await _context.Events.ToListAsync();
            if (events == null)
            {
                return null;
            }
            return events;
        }

        public async Task<Event> GetByIdAsync(int id)
        {
            var singleEvent = await _context.Events.FirstOrDefaultAsync(x => x.Id == id);
            if (singleEvent == null)
            {
                return null;
            }
            return singleEvent;

        }

        public async Task<Event> Update(Event newEvent, int id)
        {
            var eventToUpdate = await _context.Events.FindAsync(id);
            if (eventToUpdate == null)
            {
                return null;
            }

            eventToUpdate.Id = id;
            eventToUpdate.Name = newEvent.Name;
            eventToUpdate.Description = newEvent.Description;
            eventToUpdate.Venue = newEvent.Venue;
            eventToUpdate.Capacity = newEvent.Capacity;
            

            _context.Update(eventToUpdate);
            await _context.SaveChangesAsync(); 

            return newEvent;
        }
    }    
}
