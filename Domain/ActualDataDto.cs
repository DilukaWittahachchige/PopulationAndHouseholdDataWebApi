
using System;

namespace Domain
{
    public class ActualDataDto
    {
        public int Id { get; set; }
        public int StateId { get; set; }
        public double Population { get; set; }
        public double Household { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedUserId { get; set; }
        public int UpdatedUserId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
    }
}
