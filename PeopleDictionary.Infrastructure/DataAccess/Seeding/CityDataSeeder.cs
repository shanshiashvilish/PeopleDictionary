using PeopleDictionary.Core.Cities;

namespace PeopleDictionary.Infrastructure.DataAccess.Seeding
{
    public class CityDataSeeder
    {
        private readonly PeopleDictionaryDbContext _dbContext;

        public CityDataSeeder(PeopleDictionaryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (!_dbContext.Cities.Any())
            {
                _dbContext.Cities.AddRange(
                    new City { Name = "Tbilisi", DateOfCreate = DateTime.UtcNow },
                    new City { Name = "Gori", DateOfCreate = DateTime.UtcNow },
                    new City { Name = "Khashuri", DateOfCreate = DateTime.UtcNow },
                    new City { Name = "Kutaisi", DateOfCreate = DateTime.UtcNow },
                    new City { Name = "Batumi", DateOfCreate = DateTime.UtcNow }
                );

                _dbContext.SaveChanges();
            }
        }
    }
}
