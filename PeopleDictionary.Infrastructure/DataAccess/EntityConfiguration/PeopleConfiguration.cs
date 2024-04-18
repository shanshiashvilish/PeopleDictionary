using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using PeopleDictionary.Core.People;


namespace PeopleDictionary.Infrastructure.DataAccess.EntityConfiguration
{
    public class PeopleConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("people");

            builder.HasKey(p => p.Id);
            builder.HasIndex(p => p.PersonalId).IsUnique();
            builder.Property(p => p.PersonalId).IsRequired().HasMaxLength(11);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Lastname).IsRequired().HasMaxLength(50);
            builder.Property(c => c.DateOfCreate).HasDefaultValueSql("GETUTCDATE()").ValueGeneratedOnAdd();
            builder.Property(c => c.DateOfUpdate).HasDefaultValueSql("GETUTCDATE()").ValueGeneratedOnUpdate();

            builder.Property(p => p.TelNumbers).HasConversion(
                            personTelNumber => JsonConvert.SerializeObject(personTelNumber, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                            personTelNumberJson => JsonConvert.DeserializeObject<List<TelephoneNumbers>>(personTelNumberJson, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                               new ValueComparer<List<TelephoneNumbers>>(
                                   (c1, c2) => c1.SequenceEqual(c2),
                                   c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                                   c => c.ToList()));

            builder.HasMany(p => p.RelatedPeople).WithOne(rp => rp.Person).HasForeignKey(rp => rp.PersonId);
            builder.HasOne(p => p.City).WithMany().HasForeignKey(p => p.CityId);
        }
    }
}
