namespace TicketPurchaseAPI.Services
{
    public interface IQRGeneratorService
    {
       byte[] GenerateImage(string data);
    }
}
