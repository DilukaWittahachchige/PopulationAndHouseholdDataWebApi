using System.Collections.Generic;
 
namespace PopulationAndHouseholdDataWebApi.Models
{
    /// <summary>
    ///  State Query for pass state ID list
    /// </summary>
    public class StateQuery
    {
        public List<int> State { get; set; }
    }
}
