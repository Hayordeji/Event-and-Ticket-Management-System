using Microsoft.EntityFrameworkCore;
using TicketPurchaseAPI.Model;

namespace TicketPurchaseAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
    }

}
