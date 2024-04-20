using PeopleDictionary.Core.Base;
using PeopleDictionary.Core.Enums;
using PeopleDictionary.Core.People;
using PeopleDictionary.Core.RelatedPeople;

namespace PeopleDictionary.Api.Models.Responses
{
    public class GetRelationsByTypeResponse
    {
        public int Id { get; set; }
        public int? CityId { get; set; }
        public string? PersonalId { get; set; }
        public string? Name { get; set; }
        public string? Lastname { get; set; }
        public string? Image { get; set; }
        public List<RelatedPerson>? Relations { get; set; }
        public int RelationsCount { get; set; }
        public List<TelephoneNumbers>? TelNumbers { get; set; }
        public GenderEnums Gender { get; set; }
        public DateTime DateOfBirth { get; set; }


        public static Task<BaseModel<List<GetRelationsByTypeResponse>>> BuildFromList(BaseModel<List<Person>>? person)
        {
            if (person == null || person.Data == null)
            {
                return Task.FromResult(new BaseModel<List<GetRelationsByTypeResponse>>()
                {
                    IsSuccess = person?.IsSuccess ?? false,
                    StatusCode = person?.StatusCode ?? StatusCodeEnums.UnknownError,
                    Code = person?.Code ?? -1,
                    Data = new List<GetRelationsByTypeResponse>(),
                    Message = person?.Message
                });
            }

            var getByIdResponses = person.Data.Select(p => new GetRelationsByTypeResponse()
            {
                Id = p.Id,
                CityId = p.CityId,
                PersonalId = p.PersonalId,
                Name = p.Name,
                Lastname = p.Lastname,
                Image = p.Image,
                Gender = p.Gender,
                DateOfBirth = p.DateOfBirth.Date,
                TelNumbers = p.TelNumbers,
                RelationsCount = p.Relations?.Count ?? 0,
                Relations = p.Relations?.Select(rp => rp).ToList(),
            }).ToList();

            return Task.FromResult(new BaseModel<List<GetRelationsByTypeResponse>>()
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
