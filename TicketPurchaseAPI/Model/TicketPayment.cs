namespace TicketPurchaseAPI.Model
{
    public class TicketPayment
    {
        public enum Status
        {
            Pending = 1,
            Paid = 2,
        }
        public int Id { get; set; }
        public decimal Amount { get; set; } 
        public string UserId { get; set; }
        public int TicketId {  get; set; }
        public Status PaymentStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
