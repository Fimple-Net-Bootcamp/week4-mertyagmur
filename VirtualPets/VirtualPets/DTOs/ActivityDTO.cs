namespace VirtualPets.DTOs
{
    public class ActivityDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int HungerImpact { get; set; }
        public int HappinessImpact { get; set; }
        public int CleanlinessImpact { get; set; }
    }
}
