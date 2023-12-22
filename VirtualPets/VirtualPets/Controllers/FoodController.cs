using Microsoft.AspNetCore.Mvc;
using System;
using VirtualPets.Data;
using VirtualPets.Models;

namespace VirtualPets.Controllers
{
    [ApiController]
    [Route("api/v1/food")]
    public class FoodController : ControllerBase
    {
        private readonly VirtualPetDbContext context;

        public FoodController(VirtualPetDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult ListAllFood()
        {
            var foodList = context.Foods.ToList();
            return Ok(foodList);
        }

        [HttpPost("{petId}")]
        public IActionResult GiveFoodToPet(int petId, [FromBody] Food food)
        {
            var pet = context.Pets.Find(petId);
            if (pet == null)
            {
                return NotFound("Pet not found");
            }

            var petHealth = context.Health.SingleOrDefault(h => h.PetId == petId);
            if (petHealth == null)
            {
                return NotFound("Health information not found");
            }

            var petFood = context.Foods.Find(food.Id);
            if (petFood == null)
            {
                return NotFound("Food not found");
            }

            petHealth.Hunger += food.HungerImpact;
            petHealth.Happiness += food.HappinessImpact;

            petHealth.Hunger = Math.Max(0, Math.Min(petHealth.Hunger, 100));
            petHealth.Happiness = Math.Max(0, Math.Min(petHealth.Happiness, 100));

            context.SaveChanges();

            return Ok($"Pet {petId} has been fed with {food.Name}");
        }
    }
}
