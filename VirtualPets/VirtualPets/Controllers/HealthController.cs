using AutoMapper;
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

        public HealthController(VirtualPetDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
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
