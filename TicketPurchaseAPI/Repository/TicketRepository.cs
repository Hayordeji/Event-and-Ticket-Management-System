﻿
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using TicketPurchaseAPI.Data;
using TicketPurchaseAPI.Interface;
using TicketPurchaseAPI.Model;
using static TicketPurchaseAPI.Model.Ticket;
using System.Text;
using System.Drawing;
using Microsoft.AspNetCore.Identity;



namespace TicketPurchaseAPI.Repository
{
    public class TicketRepository : ITicketRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;  
        public TicketRepository(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<Ticket> ConfirmPayment(int id)
        {
            var ticket = await _context.Tickets.Include(e => e.Event).FirstOrDefaultAsync(i => i.Id == id);
            if (ticket == null)
            {
                return null;
            }
            var ticketSeller = await _userManager.FindByNameAsync(ticket.Event.Host);
            ticketSeller.Balance = ticketSeller.Balance + ((int)ticket.Price);
            ticket.Status = TicketStatus.Paid;
            ticket.Updated_At = DateTime.Now;
            ticket.Event.TicketSold++;
            await _context.SaveChangesAsync();
            return ticket;
        }

        public async Task<Ticket> CreateTicketAsync(Event eventObject,string ticketType, string buyer)
        {
                
            var newTicket = new Ticket(); 
                newTicket.Status = TicketStatus.Pending;
                newTicket.EventId = eventObject.Id;
                newTicket.Event = eventObject;
                newTicket.Updated_At = DateTime.Now;
                newTicket.BoughtBy = buyer;
                
            
            switch (ticketType)
            {
                case "Silver":
                    newTicket.Type = TicketType.Silver;
                    newTicket.Price = 25;
                    break;
                case "Gold":
                    newTicket.Type = TicketType.Gold;
                    newTicket.Price = 50;
                    break;
                case "Diamond":
                    newTicket.Type = TicketType.Diamond;
                    newTicket.Price = 100; 
                    break;
            }

            await _context.AddAsync(newTicket);
            await _context.SaveChangesAsync();
            return newTicket;
        }

        public async Task<Ticket> DeleteTicket(int id)
        {
            var ticketToDelete = await _context.Tickets.FindAsync(id);
            if (ticketToDelete == null)
            {
                return null;
            }
            _context.Remove(ticketToDelete);
            await _context.SaveChangesAsync();
            return ticketToDelete;
        }

        public async Task<Ticket> GetTicketById(int id)
        {
           var ticket = await _context.Tickets.Include(e => e.Event).FirstOrDefaultAsync(x => x.Id == id);
            if (ticket == null)
            {
                return null;
            }
            return ticket;
        }

        public async Task<List<Ticket>> GetTicketsAsync()
        {

            return await _context.Tickets.Include(e => e.Event).ToListAsync();

        }
        public async Task<bool> TicketExists(int id)
        {
            return true ? await _context.Tickets.AnyAsync(x => x.Id == id) : false;
        }

        public async Task<Ticket> VaidateTicket(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return null;
            }
            ticket.Status = TicketStatus.Validated;
            ticket.Updated_At = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return ticket;

        }
    }
}
