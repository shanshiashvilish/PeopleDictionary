using PeopleDictionary.Core.Enums;
using PeopleDictionary.Core.People;

namespace PeopleDictionary.Api.Models.Requests
{
    public class UpdatePersonRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public GenderEnums Gender { get; set; }
        public string PersonalId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int? CityId { get; set; }
        public List<TelephoneNumbers>? TelNumbers { get; set; }
    }
}
