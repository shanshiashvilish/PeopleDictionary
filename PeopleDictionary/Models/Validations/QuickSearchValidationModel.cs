using FluentValidation;
using PeopleDictionary.Api.Models.Requests;
using PeopleDictionary.Core.Resources;

namespace PeopleDictionary.Api.Models.Validations
{
    public class QuickSearchValidationModel : AbstractValidator<QuickSearchRequest>
    {
        public QuickSearchValidationModel(QuickSearchRequest model)
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(RsValidation.NameOrLastnameRequired)
                .Length(2, 50).WithMessage(RsValidation.NameOrLastNameMinMaxValue)
                .Matches(@"^[\p{L}]+$").WithMessage(RsValidation.NameOrLastNameContentWrong);

            RuleFor(x => x.Lastname)
                .NotEmpty().WithMessage(RsValidation.NameOrLastnameRequired)
                .Length(2, 50).WithMessage(RsValidation.NameOrLastNameMinMaxValue)
                .Matches(@"^[\p{L}]+$").WithMessage(RsValidation.NameOrLastNameContentWrong);

            RuleFor(x => x.PersonalId)
                .NotEmpty().WithMessage(RsValidation.PersonalIdIsRequired)
                .Length(11).WithMessage(RsValidation.PersonalIdExactValue)
                .Matches(@"^\d{11}$").WithMessage(RsValidation.PersonalIdOnlyDigits);
        }
    }
}
