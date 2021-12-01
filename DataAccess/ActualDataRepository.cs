using EF;
using EF.Models;
using IDataAccess;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess
{
    /// <summary>
    /// Actual Data Repository
    /// </summary>
    public class ActualDataRepository : GenericRepository<ActualDataEntity>, IActualDataRepository
    {
        /// <summary>
        ///  Constructer
        /// </summary>
        /// <param name="context"></param>
        public ActualDataRepository(PopulationAndHouseholdDataContext context)
           : base(context)
        {

        }

        /// <summary>
        ///  Return all Actual Data from DB
        /// </summary>
        /// <returns>ActualDataEntity</returns>
        public async Task<IEnumerable<ActualDataEntity>> LoadByStateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
