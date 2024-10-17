using TicketPurchaseAPI.Model;

namespace TicketPurchaseAPI.Services
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(AppUser user);
    }
}
