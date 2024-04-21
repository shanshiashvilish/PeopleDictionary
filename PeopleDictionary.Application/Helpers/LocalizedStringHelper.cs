using Microsoft.AspNetCore.Http;
using PeopleDictionary.Core.Resources;
using System.Globalization;

namespace PeopleDictionary.Core.Helpers
{
    public static class LocalizedStringHelper
    {
        public static string GetResourceTranslation(this string validationString, IHttpContextAccessor? httpContextAccessor)
        {
            var culture = new CultureInfo("en-US");
            if (httpContextAccessor == null)
            {
                return RsValidation.ResourceManager.GetString(validationString, culture) ?? "Something went wrong!";
            }

            var lang = httpContextAccessor.HttpContext.Request.Headers["accept-language"].ToString()
                .Split('-')[0];

            culture = lang.Equals("ka") ? new CultureInfo("ka-GE")
                : new CultureInfo("en-US");

            return RsValidation.ResourceManager.GetString(validationString, culture) ?? "Something went wrong!";
        }

    }
}
