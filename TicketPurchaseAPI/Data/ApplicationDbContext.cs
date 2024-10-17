using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TicketPurchaseAPI.Model;

namespace TicketPurchaseAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            List<IdentityRole> roles = new List<IdentityRole>
            {
                 new IdentityRole
                 {
                     Name = "Admin",
                     NormalizedName = "ADMIN"
                 },
                 new IdentityRole
                 {
                     Name = "User",
                     NormalizedName = "USER"
                 }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
    }

}
