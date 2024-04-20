using PeopleDictionary.Core.Resources;
using System.Globalization;

namespace PeopleDictionary.Core.Helpers
{
    public static class LocalizedStringHelper
    {
        public static string GetRsValidatorTranslation(this string validationString)
        {
            var culture = new CultureInfo("en-US");

            return RsValidation.ResourceManager.GetString(validationString, culture) ?? string.Empty;
        }
    }
}
