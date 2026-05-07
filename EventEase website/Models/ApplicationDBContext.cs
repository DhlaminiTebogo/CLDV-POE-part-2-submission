using Microsoft.EntityFrameworkCore;

namespace EventEase_website.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<VenueM> Venue { get; set; }
        public DbSet<EventM> Event { get; set; }
        public DbSet<BookingM> Booking { get; set; }
    }
}
