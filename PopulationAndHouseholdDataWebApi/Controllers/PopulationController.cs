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
    [Route("api/population")]
    [ApiController]
    public class PopulationController : ControllerBase
    {

        private readonly IPopulationService _populationService;

        public PopulationController(IPopulationService populationService)
        {
            this._populationService = populationService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> LoadAllActiveAsync([ModelBinder(BinderType = typeof(CustomModelBinder))] StateQuery query)
        {
    
            var populationDataList = await this._populationService.LoadAllByStateIdAsync(query?.State);

            if (populationDataList == null)
            {
                return NotFound();
            }

            return Ok(populationDataList.Select(x => ConvertToModel(x)));
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static PopulationModel ConvertToModel(PopulationDto obj)
        {
            if (obj == null)
            {
                return new PopulationModel();
            }

            return new PopulationModel()
            {
                StateId = obj.StateId,
                Population = obj.Population,
            };
        }
    }
}
