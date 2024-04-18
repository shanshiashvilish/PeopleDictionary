
using PeopleDictionary.Core.Enums;

namespace PeopleDictionary.Core.People
{
    public interface IPersonRepository
    {
        Task AddAsync(Person person);
        Task<Person> GetByIdAsync(int id);
        Task SaveChangesAsync();
        Task<IEnumerable<Person>> QuickSearchAsync(string name, string lastname, string personalId);
        Task<IEnumerable<Person>> DetailedSearchAsync(string? name, string? lastname, string? personalId, string? city, 
                                  GenderEnums gender, DateTime? dateOfBirth, DateTime? dateOfCreate, DateTime? dateOfUpdate);
        Task<IEnumerable<Person>>? GetRelatedPeopleByTypeAsync(RelationEnums relationType);
        void Remove(int id);
    }
}
