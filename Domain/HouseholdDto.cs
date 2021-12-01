 
namespace Domain
{
    // Household data domain object 
    public class HouseholdDto
    {
        public int Id { get; set; }
        public int StateId { get; set; }
        public double Household { get; set; }
        public bool IsActual { get; set; }
    }
}