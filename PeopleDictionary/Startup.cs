using Microsoft.OpenApi.Models;
using PeopleDictionary.Infrastructure.Configuration;
using PeopleDictionary.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using PeopleDictionary.Infrastructure.DataAccess.Seeding;

namespace PeopleDictionary.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PeopleDictionaryDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DbConnection")));

            services.AddScoped<CityDataSeeder>();
            services.AddPersonServices();

            services.AddHttpContextAccessor();
            services.AddHttpClient();
            services.AddMvc();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "PeopleDictionary.Api", Version = "v1" }));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CityDataSeeder cityDataSeeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PeopleDictionary.Api v1"));
            }

            cityDataSeeder.Seed();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
