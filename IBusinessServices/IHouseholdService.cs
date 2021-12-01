
#region Directives 
using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
#endregion

namespace IBusinessServices
{
    // Household Service Interface
    public interface IHouseholdService
    {
        //Load all household data by state list
        Task<IEnumerable<HouseholdDto>> LoadAllByStateIdAsync(List<int> stateIdList);
    }
}
