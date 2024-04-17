using PeopleDictionary.Core.People;
using PeopleDictionary.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;


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

        public async Task<Person> GetByIdAsync(int id)
        {
            return await _dbContext.People.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
