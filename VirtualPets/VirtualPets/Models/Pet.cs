using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace VirtualPets.Models
{
    public class Pet
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("species")]
        public string Species { get; set; }

        [Column("breed")]
        public string Breed { get; set; }

        [Column("age")]
        public int? Age { get; set; }
    }
}
