using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualPets.Models
{
    public class Activity
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("type")]
        public string Type { get; set; }

        [Column("hunger_impact")]
        public int HungerImpact { get; set; }

        [Column("happiness_impact")]
        public int HappinessImpact { get; set; }

        [Column("cleanliness_impact")]
        public int CleanlinessImpact { get; set; }
    }
}
