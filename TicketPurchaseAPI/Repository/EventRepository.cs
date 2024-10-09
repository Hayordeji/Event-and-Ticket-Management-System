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
            return newEvent;
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
            var singleEvent = await _context.Events.FindAsync(id);
            if (singleEvent == null)
            {
                return null;
            }
            return singleEvent;

        }
    }    
}
