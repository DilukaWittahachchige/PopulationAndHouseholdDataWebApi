using EF;
using IDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UnitOfWork : IAsyncDisposable, IUnitOfWork
    {
        private PopulationAndHouseholdDataContext context = new PopulationAndHouseholdDataContext
            (new DbContextOptions<PopulationAndHouseholdDataContext>());
  
        public UnitOfWork()
        {
 
        }
 
        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual async Task DisposeAsync(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    await context.DisposeAsync();
                }
            }
            this.disposed = true;
        }

        /// <summary>
        ///  Object management / Garbage collection 
        /// </summary>
        public async ValueTask DisposeAsync()
        {
            await DisposeAsync(true);
            GC.SuppressFinalize(this);
        }
    }
}
