using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF.Models
{
    /// <summary>
    /// ActualData Entity  
    /// </summary>
    public class ActualDataEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int State { get; set; }
        public double Population { get; set; }
        public double Household { get; set; }
    }
}
