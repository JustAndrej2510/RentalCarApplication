using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalCarApplication.Core.Model;

namespace RentalCarApplication.EntityFramework.Configurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.OrderId);
            builder.Property(x => x.OrderId).ValueGeneratedOnAdd();

            builder.Property(x => x.City).IsRequired();
            builder.Property(x => x.RentDate).IsRequired();
            builder.Property(x => x.ReturnDate).IsRequired();
            builder.Property(x => x.Status).HasDefaultValue(null);

            builder.HasOne(x => x.Car).WithMany(x => x.Orders).HasForeignKey(x => x.CarId);
            builder.HasOne(x => x.User).WithMany(x => x.Orders).HasForeignKey(x => x.Email);
        }
    }
}
