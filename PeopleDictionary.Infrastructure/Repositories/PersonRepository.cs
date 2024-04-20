using PeopleDictionary.Core.People;
using PeopleDictionary.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using PeopleDictionary.Core.Enums;
using PeopleDictionary.Core.Cities;
using PeopleDictionary.Core.Base;

namespace PeopleDictionary.Infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly PeopleDictionaryDbContext _dbContext;

        public PersonRepository(PeopleDictionaryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Person person)
        {
            await _dbContext.AddAsync(person);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Person>>? GetRelatedPeopleByTypeAsync(RelationEnums relationType)
        {
            return await _dbContext.People.Include(p => p.City)
                                          .Include(p => p.Relations)
                         .Where(p => p.Relations.Any(rp => rp.Type == relationType))
                         .ToListAsync();
        }

        public async Task<PagedResult<Person>?> QuickSearchAsync(string name, string lastname, string personalId, int pageNumber, int pageSize)
        {
            int totalItems = await _dbContext.People
                            .CountAsync(p =>
                                EF.Functions.Like(p.Name, $"%{name}%") &&
                                EF.Functions.Like(p.Lastname, $"%{lastname}%") &&
                                EF.Functions.Like(p.PersonalId, $"%{personalId}%"));

            var query = _dbContext.People
                            .Where(p =>
                                EF.Functions.Like(p.Name, $"%{name}%") &&
                                EF.Functions.Like(p.Lastname, $"%{lastname}%") &&
                                EF.Functions.Like(p.PersonalId, $"%{personalId}%"));

            int skipCount = (pageNumber - 1) * pageSize;

            query = query.Skip(skipCount).Take(pageSize);

            var items = await query.ToListAsync();

            return new PagedResult<Person>
            {
                Items = items,
                TotalCount = totalItems
            };
        }

        public async Task<IEnumerable<Person>> DetailedSearchAsync(string? name, string? lastname, string? personalId, string? city, GenderEnums gender, DateTime? dateOfBirth, DateTime? dateOfCreate, DateTime? dateOfUpdate)
        {
            var query = _dbContext.People.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => EF.Functions.Like(p.Name, $"%{name}%"));
            }

            if (!string.IsNullOrEmpty(lastname))
            {
                query = query.Where(p => EF.Functions.Like(p.Lastname, $"%{lastname}%"));
            }

            if (!string.IsNullOrEmpty(personalId))
            {
                query = query.Where(p => EF.Functions.Like(p.PersonalId, $"%{personalId}%"));
            }

            if (gender != null)
            {
                query = query.Where(p => p.Gender == gender);
            }

            if (dateOfBirth != null)
            {
                query = query.Where(p => p.DateOfBirth == dateOfBirth);
            }

            if (dateOfCreate != null)
            {
                query = query.Where(p => p.DateOfCreate == dateOfCreate);
            }

            if (dateOfUpdate != null)
            {
                query = query.Where(p => p.DateOfUpdate == dateOfUpdate);
            }

            return await query.ToListAsync();
        }

        public async Task<Person> GetByIdAsync(int id)
        {
            return await _dbContext.People.Include(p => p.City)
                                          .Include(p => p.Relations)
                                          .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Remove(int id)
        {
            _dbContext.Remove(id);
        }


        public async Task<City> GetCityById(int id)
        {
            return await _dbContext.Cities.FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
