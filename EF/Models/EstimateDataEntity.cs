using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF.Models
{
    /// <summary>
    /// EstimateData Entity
    /// </summary>
    public class EstimateDataEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int State { get; set; }
        public int District { get; set; }
        public double Population { get; set; }
        public double Household { get; set; }
    }
}