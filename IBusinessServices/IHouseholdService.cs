using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBusinessServices
{
    public interface IHouseholdService
    {
        Task<IEnumerable<HouseholdDto>> LoadAllByStateIdAsync(List<int> stateIdList);
    }
}
