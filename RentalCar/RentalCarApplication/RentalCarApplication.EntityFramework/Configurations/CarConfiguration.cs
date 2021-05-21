using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalCarApplication.Core.Model;

namespace RentalCarApplication.EntityFramework.Configurations
{
    internal class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasKey(x => x.CarId);
            builder.Property(x => x.CarId).ValueGeneratedNever();
            builder.Property(x => x.Brand).IsRequired().HasMaxLength(50);
            builder.Property(x => x.BodyType).IsRequired();
            builder.Property(x => x.GearBox).IsRequired();

        }
    }
}
