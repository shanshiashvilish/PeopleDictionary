using Microsoft.Extensions.DependencyInjection;
using PeopleDictionary.Application.People;
using PeopleDictionary.Core.People;
using PeopleDictionary.Infrastructure.Repositories;

namespace PeopleDictionary.Infrastructure.Configuration
{
    public static class ConfigurePersonServices
    {
        public static IServiceCollection AddPersonServices(this IServiceCollection services)
        {
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IPersonService, PersonService>();

            return services;
        }
    }
}
