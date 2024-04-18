using PeopleDictionary.Core.Enums;
using PeopleDictionary.Core.People;

namespace PeopleDictionary.Api.Models.Requests
{
    public class CreatePersonRequest
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string PersonalId { get; set; }
        public GenderEnums Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int? CityId { get; set; }
        public string? Image { get; set; }
        public List<TelephoneNumbers>? TelNumbers { get; set; }

        public static Person BuildFrom(CreatePersonRequest request)
        {
            return new Person()
            {
                Name = request.Name,
                Lastname = request.Lastname,
                PersonalId = request.PersonalId,
                Gender = request.Gender,
                DateOfBirth = request.DateOfBirth,
                CityId = request.CityId,
                Image = request.Image,
                TelNumbers = request.TelNumbers
            };
        }
    }
}
