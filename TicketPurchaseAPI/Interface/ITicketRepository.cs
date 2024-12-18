﻿using TicketPurchaseAPI.Model;

namespace TicketPurchaseAPI.Interface
{
    public interface ITicketRepository
    {
        Task<Ticket> CreateTicketAsync(Event eventObject, string ticketType, string buyer);
        Task<List<Ticket>> GetTicketsAsync();
        Task<Ticket> GetTicketById(int id);
        Task<Ticket> DeleteTicket(int id);
        Task<Ticket> ConfirmPayment(int id);
        Task<Ticket> VaidateTicket(int id); 
        Task<bool> TicketExists(int id);
    }
}
