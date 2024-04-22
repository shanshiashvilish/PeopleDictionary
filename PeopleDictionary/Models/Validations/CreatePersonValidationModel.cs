using FluentValidation;
using PeopleDictionary.Api.Models.Requests;
using PeopleDictionary.Core.Helpers;
using PeopleDictionary.Core.People;
using PeopleDictionary.Core.Resources;

namespace PeopleDictionary.Api.Models.Validations
{
    public class CreatePersonValidationModel : AbstractValidator<CreatePersonRequest>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreatePersonValidationModel(CreatePersonRequest model, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(RsValidation.NameOrLastnameRequired.GetResourceTranslation(_httpContextAccessor))
                .Length(2, 50).WithMessage(RsValidation.NameOrLastNameMinMaxValue.GetResourceTranslation(_httpContextAccessor))
                .Matches(@"^[\p{L}]+$").WithMessage(RsValidation.NameOrLastNameContentWrong.GetResourceTranslation(_httpContextAccessor));

            RuleFor(x => x.Lastname)
                .NotEmpty().WithMessage(RsValidation.NameOrLastnameRequired.GetResourceTranslation(_httpContextAccessor))
                .Length(2, 50).WithMessage(RsValidation.NameOrLastNameMinMaxValue.GetResourceTranslation(_httpContextAccessor))
                .Matches(@"^[\p{L}]+$").WithMessage(RsValidation.NameOrLastNameContentWrong.GetResourceTranslation(_httpContextAccessor));

            RuleFor(x => x.PersonalId)
                .NotEmpty().WithMessage(RsValidation.PersonalIdIsRequired.GetResourceTranslation(_httpContextAccessor))
                .Length(11).WithMessage(RsValidation.PersonalIdExactValue.GetResourceTranslation(_httpContextAccessor))
                .Matches(@"^\d{11}$").WithMessage(RsValidation.PersonalIdOnlyDigits.GetResourceTranslation(_httpContextAccessor));

            RuleFor(x => x.Gender)
                .IsInEnum().WithMessage(RsValidation.InvalidGender.GetResourceTranslation(_httpContextAccessor));

            RuleFor(x => x.DateOfBirth)
                .Must(BeAtLeast18YearsOld).WithMessage(RsValidation.PersonUnderAge.GetResourceTranslation(_httpContextAccessor));

            RuleForEach(x => x.TelNumbers)
                .SetValidator(new TelephoneNumbersValidator());
        }

        public class TelephoneNumbersValidator : AbstractValidator<TelephoneNumbers>
        {
            public TelephoneNumbersValidator()
            {
                RuleFor(x => x.Type)
                    .IsInEnum().WithMessage(RsValidation.InvalidTelNumberType);

                RuleFor(x => x.Number)
                    .NotEmpty().WithMessage(RsValidation.InvalidTelNumberType)
                    .Length(4, 50).WithMessage(RsValidation.TelNumberLenght)
                    .Matches(@"^\d+$").WithMessage(RsValidation.TelNumberOnlyDigits);
            }
        }

        #region Private Methods

        private bool BeAtLeast18YearsOld(DateTime dateOfBirth)
        {
            return dateOfBirth <= DateTime.Today.AddYears(-18);
        }

        #endregion
    }
}
