using Microsoft.AspNetCore.Http;
using PeopleDictionary.Core.Base;
using PeopleDictionary.Core.Enums;

namespace PeopleDictionary.Core.People
{
    public interface IPersonService
    {
        Task<BaseModel<bool>> CreateAsync(Person person);
        Task<BaseModel<Person?>> GetByIdAsync(int id);
        Task<BaseModel<PagedResult<Person>>>? QuickSearchAsync(string name, string lastname, string personalId, int pageNumber, int pageSize);
        Task<BaseModel<List<Person>>>? GetRelatedPeopleByTypeAsync(RelationEnums relationType);
        Task<BaseModel<bool>> UploadOrUpdateImageAsync(int personId, IFormFile file);
        Task<BaseModel<bool>> AddRelationAsync(int personId, int relatedId, RelationEnums type);
        Task<BaseModel<bool>> RemoveRelationAsync(int personId, int relationId);
        Task<BaseModel<bool>> UpdateAsync(int id, string name, string lastname, GenderEnums gender, string personalId, DateTime DateOfBirth, int? cityId, List<TelephoneNumbers>? telNumbers);
        Task<BaseModel<List<Person>>>? DetailedSearchAsync(int id, string? name, string? lastname, string? personalId, string? city, GenderEnums gender,
                                                                   DateTime? dateOfBirth, DateTime? dateOfCreate, DateTime? dateOfUpdate);
        void Remove(int id);
    }
}
