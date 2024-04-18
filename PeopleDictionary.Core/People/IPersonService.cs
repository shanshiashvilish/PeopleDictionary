using Microsoft.AspNetCore.Http;
using PeopleDictionary.Core.Enums;

namespace PeopleDictionary.Core.People
{
    public interface IPersonService
    {
        Task CreateAsync(Person person);
        Task<Person?> GetByIdAsync(int id);
        Task<List<Person>>? QuickSearchAsync(string name, string lastname, string personalId);
        Task<List<Person>>? DetailedSearchAsync(string? name, string? lastname, string? personalId, string? city, GenderEnums gender,
                                                                     DateTime? dateOfBirth, DateTime? dateOfCreate, DateTime? dateOfUpdate);
        Task<bool> UpdateAsync(int id, string name, string lastname, GenderEnums gender, string personalId, DateTime DateOfBirth, int? cityId, List<TelephoneNumbers>? telNumbers);
        Task<bool> UploadOrUpdateImageAsync(int id, IFormFile file);
        Task<bool> AddRelationAsync(int id, int relatedId, RelationEnums type);
        Task<bool> RemoveRelationAsync(int id, int relationId);
        void Remove(int id);
    }
}
