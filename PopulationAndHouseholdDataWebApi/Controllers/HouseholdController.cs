using Domain;
using IBusinessServices;
using Microsoft.AspNetCore.Mvc;
using PopulationAndHouseholdDataWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PopulationAndHouseholdDataWebApi.Controllers
{
    [Route("api/households")]
    [ApiController]
    public class HouseholdController : ControllerBase
    {

        private readonly IHouseholdService _householdService;

        public HouseholdController(IHouseholdService householdService)
        {
            this._householdService = householdService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("all-available")]
        public async Task<IActionResult> LoadAllActiveAsync()
        {
            var stateIdList = new List<int>();
            stateIdList.Add(1);
            stateIdList.Add(13);
            stateIdList.Add(26);
            stateIdList.Add(33);

            var householdDataList = await this._householdService.LoadAllByStateIdAsync(stateIdList);

            if (householdDataList == null)
            {
                return NotFound();
            }

            return Ok(householdDataList.Select(x => ConvertToModel(x)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static HouseholdModel ConvertToModel(HouseholdDto obj)
        {
            if (obj == null)
            {
                return new HouseholdModel();
            }

            return new HouseholdModel()
            {
                StateId = obj.StateId,
                Household = obj.Household,
            };
        }
    }
}
