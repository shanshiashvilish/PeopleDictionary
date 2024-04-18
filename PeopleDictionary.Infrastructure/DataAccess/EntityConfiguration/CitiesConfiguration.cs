using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PeopleDictionary.Core.Cities;

namespace PeopleDictionary.Infrastructure.DataAccess.EntityConfiguration
{
    public class CitiesConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("cities");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired();
            builder.Property(c => c.DateOfCreate).HasDefaultValueSql("GETUTCDATE()").ValueGeneratedOnAdd();
        }
    }
}
