using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using TicketPurchaseAPI.Data;
using TicketPurchaseAPI.Interface;
using TicketPurchaseAPI.Model;
using static TicketPurchaseAPI.Model.Ticket;

namespace TicketPurchaseAPI.Repository
{
    public class TicketRepository : ITicketRepository
    {
        private readonly ApplicationDbContext _context; 
        public TicketRepository(ApplicationDbContext context)
        {
            _context = context;  
        }
        public async Task<Ticket> CreateTicketAsync(int eventId,string ticketType, decimal price )
        {
            var eventObject = await _context.Events.FindAsync(eventId);
            
            var newTicket = new Ticket
            {
                Type = TicketType.Silver,  
                Status = TicketStatus.Pending,
                Event = eventObject,
                EventId = eventObject.Id,
                Price = price,
                
            };
            switch (ticketType)
            {
                case "Silver":
                    newTicket.Type = TicketType.Silver;
                    break;
                case "Gold":
                    newTicket.Type = TicketType.Gold;
                    break;
                case "Diamond":
                    newTicket.Type = TicketType.Diamond;
                    break;
            }
            await _context.AddAsync(newTicket);
            await _context.SaveChangesAsync();
            return newTicket;
        }

        public async Task<Ticket> GetTicketById(int id)
        {
           var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return null;
            }
            return ticket;
        }

        public async Task<List<Ticket>> GetTicketsAsync()
        {
            return await _context.Tickets.ToListAsync();
        }
    }
}
