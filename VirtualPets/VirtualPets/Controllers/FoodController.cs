using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System;
using VirtualPets.Data;
using VirtualPets.DTOs;
using VirtualPets.Models;

namespace VirtualPets.Controllers
{
    [ApiController]
    [Route("api/v1/food")]
    public class FoodController : ControllerBase
    {
        private readonly VirtualPetDbContext context;
        private readonly IMapper mapper;
        private readonly IValidator<FoodDTO> foodValidator;

        public FoodController(VirtualPetDbContext context, IMapper mapper, IValidator<FoodDTO> foodValidator)
        {
            this.context = context;
            this.mapper = mapper;
            this.foodValidator = foodValidator;
        }

        [HttpGet]
        public IActionResult ListAllFood()
        {
            var foodList = context.Foods.ToList();

            var foodDTOList = mapper.Map<List<FoodDTO>>(foodList);

            return Ok(foodDTOList);
        }

        [HttpPost("{petId}")]
        public IActionResult GiveFoodToPet(int petId, [FromBody] FoodDTO foodDTO)
        {
            var validationResult = foodValidator.Validate(foodDTO);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(error => error.ErrorMessage));
            }

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

            var petFood = mapper.Map<Food>(foodDTO);

            petHealth.Hunger += petFood.HungerImpact;
            petHealth.Happiness += petFood.HappinessImpact;

            petHealth.Hunger = Math.Max(0, Math.Min(petHealth.Hunger, 100));
            petHealth.Happiness = Math.Max(0, Math.Min(petHealth.Happiness, 100));

            context.SaveChanges();

            var responseDTO = $"Pet {petId} has been fed with {foodDTO.Name}";

            return Ok(responseDTO);
        }
    }
}
