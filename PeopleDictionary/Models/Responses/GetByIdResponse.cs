using PeopleDictionary.Core.Enums;
using PeopleDictionary.Core.People;
using PeopleDictionary.Core.RelatedPeople;

namespace PeopleDictionary.Api.Models.Responses
{
    public class GetByIdResponse
    {
        public int Id { get; set; }
        public int? CityId { get; set; }
        public string? PersonalId { get; set; }
        public string? Name { get; set; }
        public string? Lastname { get; set; }
        public string? Image { get; set; }
        public List<RelatedPerson>? RelatedPeople { get; set; }
        public List<TelephoneNumbers>? TelNumbers { get; set; }
        public GenderEnums Gender { get; set; }
        public DateTime DateOfBirth { get; set; }

        public static GetByIdResponse BuildFrom(Person person)
        {
            return new()
            {
                Id = person.Id,
                CityId = person.CityId,
                PersonalId = person.PersonalId,
                Name = person.Name,
                Lastname = person.Lastname,
                Image = person.Image,
                RelatedPeople = person.RelatedPeople,
                TelNumbers = person.TelNumbers,
                Gender = person.Gender,
                DateOfBirth = person.DateOfBirth.Date
            };
        }
    }
}

