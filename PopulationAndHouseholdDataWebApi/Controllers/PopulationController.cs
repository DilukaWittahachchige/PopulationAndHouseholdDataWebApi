﻿using Domain;
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
        [HttpGet("all-available")]
        public async Task<IActionResult> LoadAllActiveAsync()
        {
            var stateIdList = new List<int>();
            stateIdList.Add(1);
            stateIdList.Add(13);
            stateIdList.Add(26);
            stateIdList.Add(33);

            var populationDataList = await this._populationService.LoadAllByStateIdAsync(stateIdList);

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
