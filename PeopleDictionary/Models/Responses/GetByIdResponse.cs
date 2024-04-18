using PeopleDictionary.Core.Base;
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

        public static Task<BaseModel<GetByIdResponse>> BuildFrom(BaseModel<Person> person)
        {
            var result = new GetByIdResponse()
            {
                Id = person.Data.Id,
                CityId = person.Data.CityId,
                PersonalId = person.Data.PersonalId,
                Name = person.Data.Name,
                Lastname = person.Data.Lastname,
                Image = person.Data.Image,
                RelatedPeople = person.Data.Relations?.Select(rp => rp).ToList(),
                TelNumbers = person.Data.TelNumbers,
                Gender = person.Data.Gender,
                DateOfBirth = person.Data.DateOfBirth.Date
            };

            return Task.FromResult(new BaseModel<GetByIdResponse>()
            {
                Code = person.Code,
                Data = result,
                Message = person.Message
            });
        }
    }
}

