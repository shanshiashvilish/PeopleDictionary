using FluentValidation;
using PeopleDictionary.Api.Models.Requests;
using PeopleDictionary.Core.Helpers;
using PeopleDictionary.Core.Resources;

namespace PeopleDictionary.Api.Models.Validations
{
    public class QuickSearchValidationModel : AbstractValidator<QuickSearchRequest>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public QuickSearchValidationModel(QuickSearchRequest model, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            RuleFor(x => x.Name)
                .Length(2, 50).WithMessage(RsValidation.NameOrLastNameMinMaxValue.GetResourceTranslation(_httpContextAccessor))
                .Matches(@"^[\p{L}]+$").WithMessage(RsValidation.NameOrLastNameContentWrong);

            RuleFor(x => x.Lastname)
                .Length(2, 50).WithMessage(RsValidation.NameOrLastNameMinMaxValue)
                .Matches(@"^[\p{L}]+$").WithMessage(RsValidation.NameOrLastNameContentWrong);

            RuleFor(x => x.PersonalId)
                .Length(11).WithMessage(RsValidation.PersonalIdExactValue)
                .Matches(@"^\d{11}$").WithMessage(RsValidation.PersonalIdOnlyDigits);
        }
    }
}
