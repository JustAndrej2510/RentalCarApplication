using Microsoft.EntityFrameworkCore;
using RentalCarApplication.Core.Model;
using RentalCarApplication.EntityFramework.Configurations;

namespace RentalCarApplication.EntityFramework
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Order> Orders { get; set; }

        public ApplicationContext()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-9MRILNP\\SQLEXPRESS; Database = RentalCarDb; Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CarConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

        }
    }
}
