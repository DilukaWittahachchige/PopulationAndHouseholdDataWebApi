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
    ///  Household data business service 
    /// </summary>
    public class HouseholdService : IHouseholdService
    {
        /// <summary>
        ///   IUnitOfWork private field 
        /// </summary>
        private readonly IUnitOfWork _unityOfWork;

        /// <summary>
        ///  Population ServiceConstructer
        /// </summary>
        /// <param name="unityOfWork"></param>
        public HouseholdService(IUnitOfWork unityOfWork)
        {
            this._unityOfWork = unityOfWork;
        }

        /// <summary>
        ///  Load household details by state
        /// </summary>
        /// <param name="stateIdList"></param>
        /// <returns></returns>
        public async Task<IEnumerable<HouseholdDto>> LoadAllByStateIdAsync(List<int> stateIdList)
        {
            try
            {
                var householdList = new List<HouseholdDto>();

                //TODO : Plan to create Cache repositories for PRD

                //Call actual data from DB 
                var actualDataList = await this._unityOfWork.ActualDataRepository().GetAsync();

                //Call estimation data from DB
                var estimateDataList = await this._unityOfWork.EstimateDataRepository().GetAsync();

                stateIdList?.ForEach(y =>
                {
                    //assume no Duplicates base on given data 
                    var actualPopulation = actualDataList?.Where(x => x.State == y)?.FirstOrDefault();

                    if (actualPopulation != null)
                    {
                        //Convert entity model to domain object model
                        householdList.Add(ConvertToDomainActual(actualPopulation, true));
                    }
                    else
                    {
                        //If actual data not available then load estimation data 
                        var estimatePopulation = this.LoadEstimateHouseholds(estimateDataList, y);

                        //If estimation data found then add into the result
                        if (estimatePopulation != null)
                            householdList.Add(estimatePopulation.Result?.FirstOrDefault());
                    }
                });

                return householdList;
            }
            catch (Exception ex)
            {
                //TODO: Global exception handling 
                throw ex.InnerException;
            }
        }

        /// <summary>
        ///  Load Estimate Household
        /// </summary>
        /// <param name="estimationList"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        private async Task<List<HouseholdDto>> LoadEstimateHouseholds(IEnumerable<EstimateDataEntity> estimationList, int state)
        {

            List<HouseholdDto> result = estimationList?.Where(x => x.State == state)
                 .GroupBy(l => l.State)
                 .Select(cl => new HouseholdDto
                 {
                     StateId = cl.First().State,
                     IsActual = false,
                     Household = cl.Sum(c => c.Household),
                 })?.ToList();

            return result;
        }

        /// <summary>
        /// Convert household entity to Domain Actual model
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="isActual"></param>
        /// <returns></returns>
        private static HouseholdDto ConvertToDomainActual(ActualDataEntity obj, bool isActual)
        {
            if (obj == null)
            {
                return new HouseholdDto();
            }

            return new HouseholdDto()
            {
                Id = obj.Id,
                StateId = obj.State,
                Household = obj.Population,
                IsActual = isActual,
            };
        }

        /// <summary>
        ///  Convert To Domain Estimate
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="isActual"></param>
        /// <returns></returns>
        private static HouseholdDto ConvertToDomainEstimate(EstimateDataEntity obj, bool isActual)
        {
            if (obj == null)
            {
                return new HouseholdDto();
            }

            return new HouseholdDto()
            {
                Id = obj.Id,
                StateId = obj.State,
                Household = obj.Population,
                IsActual = isActual,
            };
        }

    }
}
