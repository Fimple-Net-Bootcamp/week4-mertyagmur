using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualPets.Models
{
    public class User
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("username")]
        public string Username { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("password")]
        public string Password { get; set; }

        public List<Pet> Pets { get; set; }
    }
}
