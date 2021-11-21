using System.Linq;

namespace EF
{
    public static class DbInitializer
    {
        public static void Initialize(PopulationAndHouseholdDataContext context)
        {
            context.Database.EnsureCreated();
            //context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            // Look for any Students.
            if (context.ActualData.Any())
            {
                return;   // DB has been seeded
            }

        }
    }
}