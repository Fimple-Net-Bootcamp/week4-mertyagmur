namespace VirtualPets.DTOs
{
    public class HealthDTO
    {
        public int Id { get; set; }
        public int PetId { get; set; }
        public int Hunger { get; set; }
        public int Happiness { get; set; }
        public int Cleanliness { get; set; }
    }
}
