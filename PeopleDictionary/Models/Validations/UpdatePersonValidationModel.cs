using FluentValidation;
using PeopleDictionary.Api.Models.Requests;
using PeopleDictionary.Core.People;
using PeopleDictionary.Core.Resources;

namespace PeopleDictionary.Api.Models.Validations
{
    public class UpdatePersonValidationModel : AbstractValidator<UpdatePersonRequest>
    {
        public UpdatePersonValidationModel(UpdatePersonRequest request)
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

            RuleFor(x => x.Gender)
                .IsInEnum().WithMessage(RsValidation.InvalidGender);

            RuleFor(x => x.DateOfBirth)
                .Must(BeAtLeast18YearsOld).WithMessage(RsValidation.PersonUnderAge);


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
