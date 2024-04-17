using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeopleDictionary.Core.People;


namespace PeopleDictionary.Infrastructure.DataAccess.EntityConfiguration
{
    public class PeopleConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("people");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.PersonalId).IsRequired().HasMaxLength(11);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Lastname).IsRequired().HasMaxLength(50);
        }
    }
}
