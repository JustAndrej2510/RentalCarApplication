using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalCarApplication.Core.Model;

namespace RentalCarApplication.EntityFramework.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Email);
            builder.HasAlternateKey(x => x.Passport);
            builder.HasAlternateKey(x => x.DriverLicense);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(20);
            builder.Property(x => x.Surname).IsRequired().HasMaxLength(22);
            builder.Property(x => x.Password).IsRequired();
            builder.Property(x => x.Passport).IsRequired();
            builder.Property(x => x.DriverLicense).IsRequired();
            builder.Property(x => x.TelNumber).IsRequired();
            builder.Property(x => x.IsAdmin).HasDefaultValue(false);
            builder.HasData(new User { Email = "andrey.pisaryk@gmail.com", Password = "GvHfmqcWyFo=", Name = "Андрей", Surname = "Писарик", Passport = "AB1234567", DriverLicense = "AA5678934", TelNumber = "+375297294012", IsAdmin = true });

        }
    }
}
