using Microsoft.EntityFrameworkCore;
using PeopleDictionary.Core.Cities;
using PeopleDictionary.Core.People;
using PeopleDictionary.Infrastructure.DataAccess.EntityConfiguration;

namespace PeopleDictionary.Infrastructure.DataAccess
{
    public class PeopleDictionaryDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Person> RelatedPeople { get; set; }
        public DbSet<City> Cities { get; set; }


        public PeopleDictionaryDbContext(DbContextOptions<PeopleDictionaryDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PeopleConfiguration());
            modelBuilder.ApplyConfiguration(new RelatedPeopleConfiguration());
            modelBuilder.ApplyConfiguration(new CitiesConfiguration());
        }
    }
}
