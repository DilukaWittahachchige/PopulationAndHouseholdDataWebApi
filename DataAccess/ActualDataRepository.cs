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
        ///  Return all active student information
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ActualDataEntity>> LoadByStateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
