using TicketPurchaseAPI.Model;

namespace TicketPurchaseAPI.Interface
{
    public interface ITicketRepository
    {
        Task<Ticket> CreateTicketAsync(int eventId, string ticketType, decimal price);
        Task<List<Ticket>> GetTicketsAsync();
        Task<Ticket> GetTicketById(int id);
    }
}
