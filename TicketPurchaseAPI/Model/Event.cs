namespace TicketPurchaseAPI.Model
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Venue { get; set; }
        public int Capacity { get; set; }
        public string UserId { get; set; }   
        public AppUser Host { get; set; }
        public int TicketSold { get; set; }
    }
}
