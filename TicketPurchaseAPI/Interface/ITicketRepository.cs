using TicketPurchaseAPI.Model;

namespace TicketPurchaseAPI.Interface
{
    public interface ITicketRepository
    {
        Task<Ticket> CreateTicketAsync(Event eventObject, string ticketType);
        Task<List<Ticket>> GetTicketsAsync();
        Task<Ticket> GetTicketById(int id);
        Task<Ticket> DeleteTicket(int id);
        
        Task<bool> TicketExists(int id);
    }
}
