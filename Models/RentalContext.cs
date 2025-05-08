using Microsoft.EntityFrameworkCore;
using CarRentalApp_MVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CarRentalApp_MVC.Models
{
    public class RentalContext : IdentityDbContext
    {
        public RentalContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Payment> Payments { get; set; }
    }
}
