namespace TicketPurchaseAPI.Dto.EventDto
{
    public class EventGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Venue { get; set; }
        public int Capacity { get; set; }
    }
}
