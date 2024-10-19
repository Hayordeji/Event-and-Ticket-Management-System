using TicketPurchaseAPI.Model;

namespace TicketPurchaseAPI.Dto.Payment
{
    public class CheckoutPaymentDto
    {
        public int amount { get; set; }
        public string tx_ref {  get; set; } = Guid.NewGuid().ToString();
        public string email { get; set; }

    }
}
