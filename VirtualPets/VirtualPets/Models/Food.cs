using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualPets.Models
{
    public class Food
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("hunger_impact")]
        public int HungerImpact { get; set; }

        [Column("happiness_impact")]
        public int HappinessImpact { get; set; }
    }
}
