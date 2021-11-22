using BusinessServices;
using DataAccess;
using EF;
using IBusinessServices;
using IDataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace DI
{
    public static class StartupExtensions
    {
        public static IConfigurationRoot Configuration { get; set; }

        public static IServiceCollection AddServiceScribeCore(this IServiceCollection services)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            services.AddDbContext<PopulationAndHouseholdDataContext>();

            //provides helpful error information in the development environment
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddControllersWithViews();
            // services.AddTransient<IEmailSender, EmailSender>();

            //DAL DI
            //ASP.Net Core includes a simple container represented by the IServiceProvider interface
            services.AddScoped<IActualDataRepository, ActualDataRepository>();
            services.AddScoped<IEstimateDataRepository, EstimateDataRepository>();
       
            //Unity of work - DAL repos 
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //BLL DI
            services.AddTransient<IPopulationService, PopulationService>();
            services.AddTransient<IHouseholdService, HouseholdService>();

            return services;
        }
    }
}
