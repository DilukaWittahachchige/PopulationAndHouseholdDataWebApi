using Domain;
using EF.Models;
using IBusinessServices;
using IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices
{
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


        public async Task<IEnumerable<PopulationDto>> LoadAllByStateIdAsync(List<int> stateIdList)
        {
            try
            {
                var populationList = new List<PopulationDto>();

                //TODO : Plan to create Cache repositories for PRD
                var actualDataList = await this._unityOfWork.ActualDataRepository().GetAsync();
                var estimateDataList = await this._unityOfWork.EstimateDataRepository().GetAsync();

                stateIdList.ForEach(y =>
                {
                    //assume no Duplicates base on given data 
                    var actualPopulation = actualDataList.Where(x =>x.State == y)?.FirstOrDefault();

                    if (actualPopulation != null)
                    {
                        populationList.Add(ConvertToDomain(actualPopulation, true));
                    }
                    else
                    {
                       // var estimationPopulationList = estimateDataList.Where(x => x.IsActive && x.StateId == y)?.ToList();
                         
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


        private async Task LoadEstimationPopulation(List<EstimateDataEntity> estimationList)
        {

            var query = estimationList.GroupBy(estimate => new { estimate.District, estimate.State,estimate.Population})
                .Select(group =>
                      new
                      {
                          StateId = group.Key.State,
                          Population = group.Sum(x => Math.Round(Convert.ToDecimal(x.Population), 2)),
                      })
                .OrderBy(group => group.StateId);
        }

        private static PopulationDto ConvertToDomain(ActualDataEntity obj, bool isActual)
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
