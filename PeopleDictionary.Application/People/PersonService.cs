using Microsoft.AspNetCore.Http;
using PeopleDictionary.Core.Enums;
using PeopleDictionary.Core.People;

namespace PeopleDictionary.Application.People
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _repository;

        public PersonService(IPersonRepository repository)
        {
            _repository = repository;
        }

        public Task<bool> AddRelationAsync(int id, int relatedId, RelationEnums type)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(Person person)
        {
            try
            {
                await _repository.AddAsync(person);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task<Person?> GetByIdAsync(int id)
        {
            try
            {
                return await _repository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return default;
            }
        }

        public Task<bool> RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveRelationAsync(int id, int relationId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(int id, string name, string lastname, GenderEnums gender, string personalId, DateTime DateOfBirth, string city, List<TelephoneNumbers> telNumbers)
        {
            var person = await GetByIdAsync(id);

            if (person == null)
            {
                return false;
            }

            person.EditData(id, name, lastname, gender, personalId, DateOfBirth, city, telNumbers);

            await _repository.SaveChangesAsync();

            return true;
        }

        public Task<bool> UploadOrUpdateImageAsync(int id, IFormFile file)
        {
            throw new NotImplementedException();
        }
    }
}
