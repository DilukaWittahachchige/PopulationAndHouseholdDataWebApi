using Domain;
using IBusinessServices;
using Microsoft.AspNetCore.Mvc;
using PopulationAndHouseholdDataWebApi.Models;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PopulationAndHouseholdDataWebApi.Controllers
{
    [Route("api/population")]
    [ApiController]
    public class PopulationController : ControllerBase
    {
        /// <summary>
        ///  IPopulationService private field
        /// </summary>
        private readonly IPopulationService _populationService;

        // constructor 
        public PopulationController(IPopulationService populationService)
        {
            this._populationService = populationService;
        }

        /// <summary>
        ///  Load All population data By State Id Async
        /// </summary>
        /// <returns>Task<IActionResult></returns>
        [HttpGet]
        public async Task<IActionResult> LoadAllByStateIdAsync([ModelBinder(BinderType = typeof(CustomModelBinder))] StateQuery query)
        {
    
            var populationDataList = await this._populationService.LoadAllByStateIdAsync(query?.State);

            //If data is not found, return code 404
            if (populationDataList == null || populationDataList.Count() == 0)
            {
                return NotFound();
            }

            return Ok(populationDataList.Select(x => ConvertToModel(x)));
        }

        /// <summary>
        /// Convert To Model
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>PopulationModel</returns>
        private static PopulationModel ConvertToModel(PopulationDto obj)
        {
            if (obj == null)
            {
                return new PopulationModel();
            }

            return new PopulationModel()
            {
                State = obj.StateId,
                Population = obj.Population,
            };
        }
    }
}
