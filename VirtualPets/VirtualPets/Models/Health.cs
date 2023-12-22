using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualPets.Models
{
    public class Health
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("pet_id")]
        public int PetId { get; set; }

        [Column("hunger")]
        public int Hunger { get; set; } = 50;

        [Column("happiness")]
        public int Happiness { get; set; } = 50;

        [Column("cleanliness")]
        public int Cleanliness { get; set; } = 50;
    }
}
