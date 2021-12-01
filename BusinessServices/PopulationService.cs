#region Directives 

using Domain;
using EF.Models;
using IBusinessServices;
using IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#endregion

namespace BusinessServices
{   
    /// <summary>
    ///  Population business service 
    /// </summary>
    public class PopulationService : IPopulationService
    {
        /// <summary>
        ///   IUnitOfWork private field 
        /// </summary>
        private readonly IUnitOfWork _unityOfWork;

        /// <summary>
        ///  Population ServiceConstructer
        /// </summary>
        /// <param name="unityOfWork"></param>
        public PopulationService(IUnitOfWork unityOfWork)
        {
            this._unityOfWork = unityOfWork;
        }

        /// <summary>
        ///  Load population details by state
        /// </summary>
        /// <param name="stateIdList"></param>
        /// <returns></returns>
        public async Task<IEnumerable<PopulationDto>> LoadAllByStateIdAsync(List<int> stateIdList)
        {
            try
            {
                var populationList = new List<PopulationDto>();

                //TODO : Plan to create Cache repositories for PRD
                var actualDataList = await this._unityOfWork.ActualDataRepository().GetAsync();
                var estimateDataList = await this._unityOfWork.EstimateDataRepository().GetAsync();

                stateIdList?.ForEach(y =>
                {
                    //assume no Duplicates base on given data 
                    var actualPopulation = actualDataList?.Where(x => x.State == y)?.FirstOrDefault();

                    if (actualPopulation != null)
                    {
                        //Convert Entity model to Domain model
                        populationList.Add(ConvertToDomainActual(actualPopulation, true));
                    }
                    else
                    {
                        //Load estimation population if actual population data not found
                        var estimatePopulation = this.LoadEstimatePopulation(estimateDataList, y);
                        if(estimatePopulation != null)
                            populationList.Add(estimatePopulation.Result?.FirstOrDefault());
                    }
                });

                return populationList;
            }
            catch (Exception ex)
            {
                //TODO: Global exception handling 
                throw ex.InnerException;
            }
        }

        /// <summary>
        ///  Load Estimate Population
        /// </summary>
        /// <param name="estimationList"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        private async Task<List<PopulationDto>> LoadEstimatePopulation(IEnumerable<EstimateDataEntity> estimationList, int state)
        {

            List<PopulationDto> result = estimationList?.Where(x => x.State == state)
                 .GroupBy(l => l.State)
                 .Select(cl => new PopulationDto
                 {
                     StateId = cl.First().State,
                     IsActual = false,
                     Population = cl.Sum(c => c.Population),
                 })?.ToList();

            return result;
        }

        /// <summary>
        ///  Convert Entity To Domain Actual
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="isActual"></param>
        /// <returns></returns>
        private static PopulationDto ConvertToDomainActual(ActualDataEntity obj, bool isActual)
        {
            if (obj == null)
            {
                return new PopulationDto();
            }

            return new PopulationDto()
            {
                Id = obj.Id,
                StateId = obj.State,
                Population = obj.Population,
                IsActual = isActual,
            };
        }

        /// <summary>
        ///  Convert To Domain Estimate
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="isActual"></param>
        /// <returns></returns>
        private static PopulationDto ConvertToDomainEstimate(EstimateDataEntity obj, bool isActual)
        {
            if (obj == null)
            {
                return new PopulationDto();
            }

            return new PopulationDto()
            {
                Id = obj.Id,
                StateId = obj.State,
                Population = obj.Population,
                IsActual = isActual,
            };
        }

    }
}
