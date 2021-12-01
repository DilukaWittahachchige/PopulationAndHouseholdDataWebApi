using System.Linq;

namespace EF
{
    public static class DbInitializer
    {
        /// <summary>
        ///  EF DB Initializer class 
        /// </summary>
        /// <param name="context"></param>
        public static void Initialize(PopulationAndHouseholdDataContext context)
        {
            context.Database.EnsureCreated();
            //context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            // Look for any Actual data .
            if (context.ActualData.Any())
            {
                return;   // DB has been seeded
            }

        }
    }
}