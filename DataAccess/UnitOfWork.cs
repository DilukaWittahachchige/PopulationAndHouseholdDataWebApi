using EF;
using IDataAccess;
using System;
using System.Threading.Tasks;

namespace DataAccess
{
    /// <summary>
    ///  Unity of work class for repository
    /// </summary>
    public class UnitOfWork : IAsyncDisposable, IUnitOfWork
    {
        /// <summary>
        /// create PopulationAndHouseholdData Context
        /// </summary>
        private PopulationAndHouseholdDataContext context = new PopulationAndHouseholdDataContext
            ();
        //Private Field for actual data repository and estimate data repositor
        private readonly IActualDataRepository _actualDataRepository;
        private readonly IEstimateDataRepository _estimateDataRepository;

        //Constructor Dependency Injection
        public UnitOfWork(
            IActualDataRepository actualDataRepository,
            IEstimateDataRepository estimateDataRepository
            )
        {
            this._actualDataRepository = actualDataRepository;
            this._estimateDataRepository = estimateDataRepository;
        }

        /// <summary>
        ///  Return Actual Data Repository
        /// </summary>
        /// <returns></returns>
        public IActualDataRepository ActualDataRepository()
        {
            return this._actualDataRepository;
        }

        /// <summary>
        ///  return estimate data repository instance
        /// </summary>
        /// <returns></returns>
        public IEstimateDataRepository EstimateDataRepository()
        {
            return this._estimateDataRepository;
        }

        // Save DB Context changes 
        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        private bool disposed = false;

        /// <summary>
        ///  Memory management | Clear unmanage resources 
        /// </summary>
        /// <param name="disposing"></param>
        /// <returns></returns>
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
