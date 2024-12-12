using BeestjeOpJeFeestje.Business.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BeestjeOpJeFeestje.Data.DbContext
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //public DbSet<Animal> Animals { get; set; }
        //public DbSet<Account> Accounts { get; set; }
        //public DbSet<Booking> Bookings { get; set; }
    }
}
