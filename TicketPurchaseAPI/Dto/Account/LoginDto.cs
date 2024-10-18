using System.ComponentModel.DataAnnotations;

namespace TicketPurchaseAPI.Dto.Account
{
    public class LoginDto
    {
        
        [Required]
        public string EmailOrUsername { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
