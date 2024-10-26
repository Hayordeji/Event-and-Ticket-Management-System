using Microsoft.AspNetCore.Identity;

namespace TicketPurchaseAPI.Model
{
    public class AppUser : IdentityUser
    {
        public int Balance { get; set; }
    }
}
