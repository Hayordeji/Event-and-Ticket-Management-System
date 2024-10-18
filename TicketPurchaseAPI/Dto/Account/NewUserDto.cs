using System.ComponentModel.DataAnnotations;

namespace TicketPurchaseAPI.Dto.Account
{
    public class NewUserDto
    {
        
        public string Username { get; set; }
        public string Email { get; set; }
        public string JwtToken { get; set; }

    }
}
