namespace TicketPurchaseAPI.Model
{
    public class Ticket
    {
        public enum TicketType
        {
            Silver = 1,
            Gold = 2,
            Diamond = 3
        }

        public enum TicketStatus
        {
            Pending = 1,
            Paid = 2,
            Validated = 3
        }



        public int Id { get; set; }
        public TicketType Type { get; set; }
        public TicketStatus Status { get; set; }
        public Event Event { get; set; }
        public int EventId { get; set; } 
        public decimal Price { get; set; }
        public DateTime Created_At { get; set; } = DateTime.Now;
        public DateTime? Updated_At { get; set; } 



    }
}
