using FluentValidation;
using PeopleDictionary.Api.Models.Requests;
using PeopleDictionary.Core.Enums;
using PeopleDictionary.Core.People;

namespace PeopleDictionary.Api.Models.Validations
{
    public class CreatePersonValidationModel : AbstractValidator<CreatePersonRequest>
    {
        public CreatePersonValidationModel(CreatePersonRequest model)
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(2, 50).WithMessage("Name must be between 2 and 50 characters.")
                .Matches(@"^[\p{L}]+$").WithMessage("Name must contain only Georgian or English symbols.");

            RuleFor(x => x.Lastname)
                .NotEmpty().WithMessage("Lastname is required.")
                .Length(2, 50).WithMessage("Lastname must be between 2 and 50 characters.")
                .Matches(@"^[\p{L}]+$").WithMessage("Lastname must contain only Georgian or English symbols.");

            RuleFor(x => x.PersonalId)
                .NotEmpty().WithMessage("PersonalId is required.")
                .Length(11).WithMessage("PersonalId must be exactly 11 digits.")
                .Matches(@"^\d{11}$").WithMessage("PersonalId must contain only digits.");

            RuleFor(x => x.Gender)
                .IsInEnum().WithMessage("Invalid gender.");

            RuleFor(x => x.DateOfBirth)
                .Must(BeAtLeast18YearsOld).WithMessage("Date of birth must correspond to someone who is at least 18 years old.");

            RuleForEach(x => x.TelNumbers)
                .SetValidator(new TelephoneNumbersValidator());
        }

        public class TelephoneNumbersValidator : AbstractValidator<TelephoneNumbers>
        {
            public TelephoneNumbersValidator()
            {
                RuleFor(x => x.Type)
                    .IsInEnum().WithMessage("Invalid telephone number type.");

                RuleFor(x => x.Number)
                    .NotEmpty().WithMessage("Telephone number is required.")
                    .Length(4, 50).WithMessage("Telephone number must be between 4 and 50 characters.")
                    .Matches(@"^\d+$").WithMessage("Telephone number must contain only digits.");
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
