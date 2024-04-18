using Microsoft.AspNetCore.Http;
using PeopleDictionary.Core.Cities;
using PeopleDictionary.Core.Enums;
using PeopleDictionary.Core.People;
using PeopleDictionary.Core.RelatedPeople;

namespace PeopleDictionary.Application.People
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _repository;

        public PersonService(IPersonRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateAsync(Person person)
        {
            try
            {
                await _repository.AddAsync(person);
                await _repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task<bool> UpdateAsync(int id, string name, string lastname, GenderEnums gender, string personalId, DateTime DateOfBirth, int? cityId, List<TelephoneNumbers>? telNumbers)
        {
            var person = await GetByIdAsync(id);

            if (person == null)
            {
                return false;
            }

            City city = new(); // TO DO: get city by id

            person.EditData(id, name, lastname, gender, personalId, DateOfBirth, city, telNumbers);

            await _repository.SaveChangesAsync();

            return true;
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

        public async Task<bool> UploadOrUpdateImageAsync(int id, IFormFile file)
        {
            try
            {
                var person = await GetByIdAsync(id);

                if (person == null)
                {
                    return false;
                }

                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string imgFolderPath = Path.Combine(desktopPath, "img");

                if (!Directory.Exists(imgFolderPath))
                {
                    Directory.CreateDirectory(imgFolderPath);
                }

                string fileExtension = Path.GetExtension(file.FileName);
                string fileName = $"{id}{fileExtension}";
                string filePath = Path.Combine(imgFolderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                person.Image = filePath;
                await _repository.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> AddRelationAsync(int id, int relatedId, RelationEnums type)
        {
            try
            {
                var person = await _repository.GetByIdAsync(id);
                var relatedPerson = await _repository.GetByIdAsync(relatedId);

                if (person == null || relatedPerson == null)
                {
                    return false;
                }

                person.AddRelatedPerson(new RelatedPerson { Person = person, });

                await _repository.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> RemoveRelationAsync(int id, int relationId)
        {
            try
            {
                var person = await _repository.GetByIdAsync(id);

                if (person == null || person.RelatedPeople == null)
                {
                    return false;
                }

                person.RemoveRelatedPerson(relationId);
                await _repository.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<List<Person>>? QuickSearchAsync(string name, string lastname, string personalId)
        {
            try
            {
                if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(lastname) && string.IsNullOrEmpty(personalId))
                {
                    return default;
                }

                var result = await _repository.QuickSearchAsync(name, lastname, personalId);

                if (!result.Any())
                {
                    return default;
                }

                return result.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return default;
            }
        }

        public async Task<List<Person>>? DetailedSearchAsync(string? name, string? lastname, string? personalId, string? city, GenderEnums gender,
                                                             DateTime? dateOfBirth, DateTime? dateOfCreate, DateTime? dateOfUpdate)
        {
            try
            {
                // Check if all search parameters are empty or null
                if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(lastname) && string.IsNullOrEmpty(personalId) &&
                    string.IsNullOrEmpty(city) && dateOfBirth == null && dateOfCreate == null && dateOfUpdate == null)
                {
                    return default;
                }

                var result = await _repository.DetailedSearchAsync(name, lastname, personalId, city, gender, dateOfBirth, dateOfCreate, dateOfUpdate);

                if (!result.Any())
                {
                    return default;
                }

                return result.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return default;
            }
        }

        public async Task<List<Person>>? GetRelatedPeopleByTypeAsync(RelationEnums relationType)
        {
            try
            {
                return (await _repository.GetRelatedPeopleByTypeAsync(relationType)).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return default;
            }
        }


        public void Remove(int id)
        {
            try
            {
                _repository.Remove(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
