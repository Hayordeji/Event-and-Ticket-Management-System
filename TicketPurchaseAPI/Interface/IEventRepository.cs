using TicketPurchaseAPI.Dto;
using TicketPurchaseAPI.Model;

namespace TicketPurchaseAPI.Interface
{
    public interface IEventRepository
    {
        Task<List<Event>> GetAsync();
        Task<Event> GetByIdAsync(int id);
        Task<Event> Create(Event newEvent);
        Task<Event> Update(Event newEvent, int id);
        Task<Event> Delete(int id);
    }
}
