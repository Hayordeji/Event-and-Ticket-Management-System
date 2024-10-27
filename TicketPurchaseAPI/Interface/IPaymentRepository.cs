using TicketPurchaseAPI.Model;
namespace TicketPurchaseAPI.Interface
{
    public interface IPaymentRepository
    {
        Task<TicketPayment> CreatePaymentAsync(string user, int ticketId, decimal amount);
        Task<List<TicketPayment>> GetPaymentsAsync();
        Task<TicketPayment> GetPaymentByIdAsync(int id);

    }
}
