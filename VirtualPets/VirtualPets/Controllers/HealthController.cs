using Microsoft.AspNetCore.Mvc;
using System;
using VirtualPets.Data;
using VirtualPets.Models;

namespace VirtualPets.Controllers
{
    [ApiController]
    [Route("api/v1/health")]
    public class HealthController : ControllerBase
    {
        private readonly VirtualPetDbContext context;

        public HealthController(VirtualPetDbContext context)
        {
            this.context = context;
        }

        [HttpGet("{petId}")]
        public IActionResult GetPetHealth(int petId)
        {
            var pet = context.Pets.Find(petId);
            if (pet == null)
            {
                return NotFound("Pet not found");
            }

            var health = context.Health.SingleOrDefault(h => h.PetId == petId);
            if (health == null)
            {
                return NotFound("Health information not found");
            }

            return Ok(health);
        }

        [HttpPatch("{petId}")]
        public IActionResult UpdatePetHealth(int petId, [FromBody] Health updatedHealth)
        {
            var pet = context.Pets.Find(petId);
            if (pet == null)
            {
                return NotFound("Pet not found");
            }

            var health = context.Health.SingleOrDefault(h => h.PetId == petId);
            if (health == null)
            {
                return NotFound("Health information not found");
            }

            health.Hunger = updatedHealth.Hunger;
            health.Happiness = updatedHealth.Happiness;
            health.Cleanliness = updatedHealth.Cleanliness;

            context.SaveChanges();

            return Ok(health);
        }
    }
}
