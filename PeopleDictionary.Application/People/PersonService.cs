using Microsoft.AspNetCore.Http;
using PeopleDictionary.Core.Base;
using PeopleDictionary.Core.Enums;
using PeopleDictionary.Core.People;
using PeopleDictionary.Core.RelatedPeople;
using System;

namespace PeopleDictionary.Application.People
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _repository;

        public PersonService(IPersonRepository repository)
        {
            _repository = repository;
        }

        public async Task<BaseModel<bool>> CreateAsync(Person person)
        {
            try
            {
                await _repository.AddAsync(person);

                return new BaseModel<bool>(true, default, string.Empty);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new BaseModel<bool>(false, default, "Unexpected Error!");
            }
        }

        public async Task<BaseModel<bool>> UpdateAsync(int id, string name, string lastname, GenderEnums gender, string personalId, DateTime DateOfBirth, int? cityId, List<TelephoneNumbers>? telNumbers)
        {
            try
            {
                var person = await GetByIdAsync(id);

                if (person == null || person.Data == null)
                {
                    return new BaseModel<bool>(false, default, $"Unable to find person with id {id}");
                }

                if (cityId == null)
                {
                    return new BaseModel<bool>(false, default, "City ID is not provided");
                }

                var city = await _repository.GetCityById(cityId ?? -1);

                if (city == null)
                {
                    return new BaseModel<bool>(false, default, $"Unable to find a city with id {cityId}");
                }

                person.Data.EditData(id, name, lastname, gender, personalId, DateOfBirth, city, telNumbers);
                await _repository.SaveChangesAsync();

                return new BaseModel<bool>(true, default, string.Empty);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new BaseModel<bool>(false, default, $"Unexpected error!");
            }
        }

        public async Task<BaseModel<Person?>> GetByIdAsync(int id)
        {
            try
            {
                var person = await _repository.GetByIdAsync(id);

                if (person == null)
                {
                    return new BaseModel<Person?>(false, default, $"Unable to find person with id {id}");
                }

                return new BaseModel<Person?>(true, person, string.Empty);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return default;
            }
        }

        public async Task<BaseModel<bool>> UploadOrUpdateImageAsync(int id, IFormFile file)
        {
            try
            {
                var person = await GetByIdAsync(id);

                if (person == null || person.Data == null)
                {
                    return new BaseModel<bool>(false, default, $"Unable to find person with id {id}");
                }

                var uploadResult = await UploadImageAsync(person.Data, file);

                if (!uploadResult)
                {
                    return new BaseModel<bool>(false, default, $"Image upload process was failed. Person id: {id}");
                }

                return new BaseModel<bool>(true, default, string.Empty);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new BaseModel<bool>(false, default, $"Unexpected error!");
            }
        }

        public async Task<BaseModel<bool>> AddRelationAsync(int id, int relatedId, RelationEnums type)
        {
            try
            {
                var person = await GetByIdAsync(id);
                var relatedPerson = await GetByIdAsync(relatedId);

                if (person == null || person.Data == null || relatedPerson == null || relatedPerson.Data == null)
                {
                    return new BaseModel<bool>(false, default, "Unable to find the person or the related one");
                }

                person.Data.AddRelation(new RelatedPerson { Person = person.Data, RelatedTo = relatedPerson.Data, Type = type });

                await _repository.SaveChangesAsync();

                return new BaseModel<bool>(true, default, string.Empty);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new BaseModel<bool>(false, default, $"Unexpected error!");
            }
        }

        public async Task<BaseModel<bool>> RemoveRelationAsync(int personId, int relationId)
        {
            try
            {
                var person = await GetByIdAsync(personId);

                if (person == null || person.Data == null || person.Data.Relations == null)
                {
                    return new BaseModel<bool>(false, default, "Unable to find the person or the related one");
                }

                person.Data.RemoveRelation(relationId);
                await _repository.SaveChangesAsync();

                return new BaseModel<bool>(true, default, string.Empty);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new BaseModel<bool>(false, default, $"Unexpected error!");
            }
        }

        public async Task<BaseModel<List<Person>>>? QuickSearchAsync(string name, string lastname, string personalId)
        {
            try
            {
                if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(lastname) && string.IsNullOrEmpty(personalId))
                {
                    return new BaseModel<List<Person>>(false, default, "All the search criterias are empty");
                }

                var result = await _repository.QuickSearchAsync(name, lastname, personalId);

                if (!result.Any())
                {
                    return new BaseModel<List<Person>>(false, default, "No matching records found with provided fields");
                }

                return new BaseModel<List<Person>>(true, result.ToList(), string.Empty);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new BaseModel<List<Person>>(false, default, $"Unexpected error!");
            }
        }

        public async Task<BaseModel<List<Person>>>? DetailedSearchAsync(int id, string? name, string? lastname, string? personalId, string? city, GenderEnums gender,
                                                             DateTime? dateOfBirth, DateTime? dateOfCreate, DateTime? dateOfUpdate)
        {
            try
            {
                // Check if all search parameters are empty or null
                if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(lastname) && string.IsNullOrEmpty(personalId) &&
                    string.IsNullOrEmpty(city) && dateOfBirth == null && dateOfCreate == null && dateOfUpdate == null)
                {
                    return new BaseModel<List<Person>>(false, default, "All the search criterias are empty");
                }

                var result = await _repository.DetailedSearchAsync(name, lastname, personalId, city, gender, dateOfBirth, dateOfCreate, dateOfUpdate);

                if (!result.Any())
                {
                    return new BaseModel<List<Person>>(false, default, "No matching records found with provided fields");
                }

                return new BaseModel<List<Person>>(true, result.ToList(), string.Empty);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new BaseModel<List<Person>>(false, default, $"Unexpected error!");
            }
        }

        public async Task<BaseModel<List<Person>>>? GetRelatedPeopleByTypeAsync(RelationEnums relationType)
        {
            try
            {
                var result = (await _repository.GetRelatedPeopleByTypeAsync(relationType)).ToList();

                if (result != null && result.Count > 0)
                {
                    return new BaseModel<List<Person>>(false, default, "Unable to find records with provided type");
                }

                return new BaseModel<List<Person>>(true, result.ToList(), string.Empty);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new BaseModel<List<Person>>(false, default, $"Unexpected error!");
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


        #region Private Methods

        public async Task<bool> UploadImageAsync(Person person, IFormFile file)
        {
            try
            {
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string imgFolderPath = Path.Combine(desktopPath, "img");

                if (!Directory.Exists(imgFolderPath))
                {
                    Directory.CreateDirectory(imgFolderPath);
                }

                string fileExtension = Path.GetExtension(file.FileName);
                string fileName = $"{person.Id}{fileExtension}";
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

        #endregion
    }
}
