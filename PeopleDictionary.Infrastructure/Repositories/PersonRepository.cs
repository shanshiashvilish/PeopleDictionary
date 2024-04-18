using PeopleDictionary.Core.People;
using PeopleDictionary.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using PeopleDictionary.Core.Enums;

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
        }

        public async Task<IEnumerable<Person>>? GetRelatedPeopleByTypeAsync(RelationEnums relationType)
        {
            return await _dbContext.People.Include(p => p.City)
                                          .Include(p => p.RelatedPeople)
                         .Where(p => p.RelatedPeople.Any(rp => rp.RelationType == relationType))
                         .ToListAsync();
        }

        public async Task<IEnumerable<Person>> QuickSearchAsync(string name, string lastname, string personalId)
        {
            return await _dbContext.People
                            .Where(p =>
                                EF.Functions.Like(p.Name, $"%{name}%") &&
                                EF.Functions.Like(p.Lastname, $"%{lastname}%") &&
                                EF.Functions.Like(p.PersonalId, $"%{personalId}%"))
                            .ToListAsync();
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
            return await _dbContext.People.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Remove(int id)
        {
            _dbContext.Remove(id);
        }
    }
}
