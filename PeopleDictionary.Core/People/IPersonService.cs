using Microsoft.AspNetCore.Http;
using PeopleDictionary.Core.Enums;

namespace PeopleDictionary.Core.People
{
    public interface IPersonService
    {
        Task CreateAsync(Person person);
        Task<Person?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(int id, string name, string lastname, GenderEnums gender, string personalId, DateTime DateOfBirth, string city, List<TelephoneNumbers> telNumbers);
        Task<bool> UploadOrUpdateImageAsync(int id, IFormFile file);
        Task<bool> AddRelationAsync(int id, int relatedId, RelationEnums type);
        Task<bool> RemoveRelationAsync(int id, int relationId);
        Task<bool> RemoveAsync(int id);
    }
}
