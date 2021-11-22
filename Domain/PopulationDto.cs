
using System;

namespace Domain
{
    public class PopulationDto
    {
        public int Id { get; set; }
        public int StateId { get; set; }
        public double Population { get; set; }
        public bool IsActual { get; set; }
    }
}
