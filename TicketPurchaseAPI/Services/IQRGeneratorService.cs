using TicketPurchaseAPI.Model;

namespace TicketPurchaseAPI.Services
{
    public interface IQRGeneratorService
    {
       Task<byte[]> GenerateImage(Ticket ticketData);
    }
}
