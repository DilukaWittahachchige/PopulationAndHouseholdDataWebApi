#region Directives 
using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
#endregion

namespace IBusinessServices
{
    /// <summary>
    ///  Population Service interface
    /// </summary>
    public interface IPopulationService
    {
        //Load all population data by state list
        Task<IEnumerable<PopulationDto>> LoadAllByStateIdAsync(List<int> stateIdList);
    }
}
