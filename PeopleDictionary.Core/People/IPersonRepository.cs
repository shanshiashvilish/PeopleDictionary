
namespace PeopleDictionary.Core.People
{
    public interface IPersonRepository
    {
        Task AddAsync(Person person);
        Task<Person> GetByIdAsync(int id);
        Task SaveChangesAsync();
    }
}
