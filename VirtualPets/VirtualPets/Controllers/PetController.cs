using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VirtualPets.Data;
using VirtualPets.Models;

namespace VirtualPets.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly VirtualPetDbContext context;

        public PetController(VirtualPetDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public IActionResult CreatePet([FromBody] Pet pet)
        {
            if (context.Users.Find(pet.UserId) == null)
            {
                return NotFound("User not found");
            }

            context.Pets.Add(pet);
            context.SaveChanges();
            return CreatedAtAction(nameof(GetPet), new { petId = pet.Id }, pet);
        }

        [HttpGet]
        public IActionResult ListPets()
        {
            var pets = context.Pets.ToList();
            return Ok(pets);
        }

        [HttpGet("{petId}")]
        public IActionResult GetPet(int petId)
        {
            var pet = context.Pets.Find(petId);
            if (pet == null)
            {
                return NotFound();
            }
            return Ok(pet);
        }

        [HttpPut("{petId}")]
        public IActionResult UpdatePet(int petId, [FromBody] Pet updatedPet)
        {
            var pet = context.Pets.Find(petId);
            if (pet == null)
            {
                return NotFound();
            }

            pet.UserId = updatedPet.UserId;
            pet.Name = updatedPet.Name;
            pet.Species = updatedPet.Species;
            pet.Breed = updatedPet.Breed;
            pet.Age = updatedPet.Age;

           context.SaveChanges();

            return Ok(pet);
        }
    }
}
