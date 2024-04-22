using Microsoft.AspNetCore.Http;
using PeopleDictionary.Core.Resources;
using System.Globalization;

namespace PeopleDictionary.Core.Helpers
{
    public static class LocalizedStringHelper
    {
        public static string GetResourceTranslation(this string validationString, IHttpContextAccessor httpContextAccessor)
        {
            var culture = CultureInfo.InvariantCulture;

            if (httpContextAccessor != null && httpContextAccessor.HttpContext != null)
            {
                var lang = httpContextAccessor.HttpContext.Request.Headers["accept-language"].ToString()
                    .Split('-')[0];

                culture = lang.Equals("ka") ? new CultureInfo("ka-GE") : new CultureInfo("en-US");
            }

            var translatedString = RsValidation.ResourceManager.GetString(validationString, culture);
            if (translatedString == null)
            {
                Console.WriteLine($"Resource key '{validationString}' not found for culture '{culture.Name}'");
            }

            return translatedString ?? "Something went wrong!";
        }
    }
}
