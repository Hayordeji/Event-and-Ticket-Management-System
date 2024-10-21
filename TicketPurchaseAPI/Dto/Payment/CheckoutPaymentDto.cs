using TicketPurchaseAPI.Model;

namespace TicketPurchaseAPI.Dto.Payment
{
    public class CheckoutPaymentDto
    {
        public int amount { get; set; }
        public string currency { get; set; }
        public string tx_ref {  get; set; } 
        public string redirect_url { get; set; }   
        public Customer customer { get; set; }
        

    }
}
