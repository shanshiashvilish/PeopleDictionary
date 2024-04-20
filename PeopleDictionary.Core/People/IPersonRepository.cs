using PeopleDictionary.Core.Base;
using PeopleDictionary.Core.Cities;
using PeopleDictionary.Core.Enums;

namespace PeopleDictionary.Core.People
{
    public interface IPersonRepository
    {
        Task AddAsync(Person person);
        Task<Person> GetByIdAsync(int id);
        Task SaveChangesAsync();
        Task<PagedResult<Person>?> QuickSearchAsync(string name, string lastname, string personalId, int pageNumber, int pageSize);
        Task<IEnumerable<Person>> DetailedSearchAsync(string? name, string? lastname, string? personalId, string? city, 
                                  GenderEnums gender, DateTime? dateOfBirth, DateTime? dateOfCreate, DateTime? dateOfUpdate);
        Task<IEnumerable<Person>>? GetRelatedPeopleByTypeAsync(RelationEnums relationType);
        void Remove(int id);

        Task<City> GetCityById(int id);
    }
}
