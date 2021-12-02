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
                    var actualHousehold = actualDataList?.Where(x => x.State == y).Distinct()?.FirstOrDefault();

                    if (actualHousehold != null)
                    {
                        //Convert entity model to domain object model
                        householdList.Add(ConvertToDomainActual(actualHousehold, true));
                    }
                    else
                    {
                        //If actual data not available then load estimation data 
                        var estimateHousehold = this.LoadEstimateHouseholds(estimateDataList, y);

                        //If estimation data found then add into the result
                        if (estimateHousehold != null && estimateHousehold.Result.Count > 0)
                            householdList.Add(estimateHousehold.Result?.FirstOrDefault());
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
            //return a sum of the value over all districts for the required state in the Estimates table
            List<HouseholdDto> result = estimationList?.Where(x => x.State == state)
                 .GroupBy(l => l.State)
                 .Select(cl => new HouseholdDto
                 {
                     StateId = cl.First().State,
                     IsActual = false,
                     Household = cl.Sum(c => c.Household),
                 })?.Distinct()?.ToList();

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
                Household = obj.Household,
                IsActual = isActual,
            };
        }
 
    }
}
