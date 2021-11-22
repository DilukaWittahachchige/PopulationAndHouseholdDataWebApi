using System;

namespace Domain
{
    public class HouseholdDto
    {
        public int Id { get; set; }
        public int StateId { get; set; }
        public double Household { get; set; }
        public bool IsActual { get; set; }
    }
}