using Domain;
using IBusinessServices;
using Microsoft.AspNetCore.Mvc;
using PopulationAndHouseholdDataWebApi.Models;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace PopulationAndHouseholdDataWebApi.Controllers
{
    [Route("api/households")]
    [ApiController]
    public class HouseholdController : ControllerBase
    {
        /// <summary>
        /// IHouseholdService private field
        /// </summary>
        private readonly IHouseholdService _householdService;

        /// <summary>
        /// Household Controller constructor 
        /// </summary>
        /// <param name="householdService"></param>
        public HouseholdController(IHouseholdService householdService)
        {
            this._householdService = householdService;
        }

        /// <summary>
        /// Load All By State Id Async
        /// </summary>
        /// <returns>ask<IActionResult></returns>
        [HttpGet]
        public async Task<IActionResult> LoadAllByStateIdAsync([ModelBinder(BinderType = typeof(CustomModelBinder))] StateQuery query)
        {
     
            var householdDataList = await this._householdService.LoadAllByStateIdAsync(query?.State);

            //If data is not found, return code 404
            if (householdDataList == null || householdDataList.Count() == 0)
            {
                return NotFound();
            }

            return Ok(householdDataList.Select(x => ConvertToModel(x)));
        }

        /// <summary>
        ///  Convert To Model
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
                State = obj.StateId,
                Household = obj.Household,
            };
        }
    }
}
