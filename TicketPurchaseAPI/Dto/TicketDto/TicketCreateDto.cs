namespace TicketPurchaseAPI.Dto.TicketDto
{
    public class TicketCreateDto
    {
        public string Type { get; set; }
        public decimal Price { get; set; }
        public int EventId { get; set; }
    }
}
