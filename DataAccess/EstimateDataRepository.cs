using EF;
using EF.Models;
using IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class EstimateDataRepository : GenericRepository<EstimateDataEntity>, IEstimateDataRepository
    {
        /// <summary>
        ///  Constructer
        /// </summary>
        /// <param name="context"></param>
        public EstimateDataRepository(PopulationAndHouseholdDataContext context)
           : base(context)
        {

        }

        /// <summary>
        ///  Return all active student information
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<EstimateDataEntity>> LoadByStateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
