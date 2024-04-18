using PeopleDictionary.Core.Base;
using PeopleDictionary.Core.Enums;
using PeopleDictionary.Core.People;
using PeopleDictionary.Core.RelatedPeople;

namespace PeopleDictionary.Api.Models.Responses
{
    public class GetPersonResponse
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

        public static Task<BaseModel<GetPersonResponse>> BuildFrom(BaseModel<Person> person)
        {
            var result = new GetPersonResponse()
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

            return Task.FromResult(new BaseModel<GetPersonResponse>()
            {
                IsSuccess = person.IsSuccess,
                StatusCode = person.StatusCode,
                Code = person.Code,
                Data = result,
                Message = person.Message
            });
        }

        public static Task<BaseModel<List<GetPersonResponse>>> BuildFromList(BaseModel<List<Person>>? person)
        {
            if (person == null || person.Data == null)
            {
                return Task.FromResult(new BaseModel<List<GetPersonResponse>>()
                {
                    IsSuccess = person?.IsSuccess ?? false,
                    StatusCode = person?.StatusCode ?? StatusCodeEnums.UnknownError,
                    Code = person?.Code ?? -1,
                    Data = new List<GetPersonResponse>(),
                    Message = person?.Message
                });
            }

            var getByIdResponses = person.Data.Select(p => new GetPersonResponse()
            {
                Id = p.Id,
                CityId = p.CityId,
                PersonalId = p.PersonalId,
                Name = p.Name,
                Lastname = p.Lastname,
                Image = p.Image,
                RelatedPeople = p.Relations?.Select(rp => rp).ToList(),
                TelNumbers = p.TelNumbers,
                Gender = p.Gender,
                DateOfBirth = p.DateOfBirth.Date
            }).ToList();

            return Task.FromResult(new BaseModel<List<GetPersonResponse>>()
            {
                IsSuccess = person.IsSuccess,
                StatusCode = person.StatusCode,
                Code = person.Code,
                Data = getByIdResponses,
                Message = person.Message
            });
        }

    }
}

