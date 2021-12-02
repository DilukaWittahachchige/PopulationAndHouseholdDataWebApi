#region Directives 

using BusinessServices;
using DataAccess;
using EF;
using IBusinessServices;
using IDataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace DI
{
    /// <summary>
    ///  C# Extension class for DI registration
    ///  DI registration class in common layer
    /// </summary>
    public static class StartupExtensions
    {
        public static IConfigurationRoot Configuration { get; set; }

        //Create extension method for DI registration
        public static IServiceCollection AddServiceScribeCore(this IServiceCollection services)
        {
            // EF DB Context registration
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

            //BLL DI registration
            services.AddTransient<IPopulationService, PopulationService>();
            services.AddTransient<IHouseholdService, HouseholdService>();

            return services;
        }
    }
}
