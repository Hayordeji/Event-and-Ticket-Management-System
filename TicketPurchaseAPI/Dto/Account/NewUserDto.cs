using System.ComponentModel.DataAnnotations;

namespace TicketPurchaseAPI.Dto.Account
{
    public class NewUserDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string JwtToken { get; set; }

    }
}
