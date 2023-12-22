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
    [Route("api/v1/health")]
    public class HealthController : ControllerBase
    {
        private readonly VirtualPetDbContext context;
        private readonly IMapper mapper;
        private readonly IValidator<HealthDTO> healthValidator;

        public HealthController(VirtualPetDbContext context, IMapper mapper, IValidator<HealthDTO> healthValidator)
        {
            this.context = context;
            this.mapper = mapper;
            this.healthValidator = healthValidator;
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

            var healthDTO = mapper.Map<HealthDTO>(health);

            return Ok(healthDTO);
        }

        [HttpPatch("{petId}")]
        public IActionResult UpdatePetHealth(int petId, [FromBody] HealthDTO updatedHealthDTO)
        {
            var validationResult = healthValidator.Validate(updatedHealthDTO);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(error => error.ErrorMessage));
            }

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

            mapper.Map(updatedHealthDTO, health);

            context.SaveChanges();

            var responseDTO = mapper.Map<HealthDTO>(health);

            return Ok(responseDTO);
        }
    }
}
