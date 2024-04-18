using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PeopleDictionary.Core.RelatedPeople;

namespace PeopleDictionary.Infrastructure.DataAccess.EntityConfiguration
{
    public class RelatedPeopleConfiguration : IEntityTypeConfiguration<RelatedPerson>
    {
        public void Configure(EntityTypeBuilder<RelatedPerson> builder)
        {
            builder.ToTable("related_people");
            
            builder.HasKey(rp => rp.Id);

            builder.Property(c => c.DateOfCreate).HasDefaultValueSql("GETUTCDATE()").ValueGeneratedOnAdd();
        }
    }
}
