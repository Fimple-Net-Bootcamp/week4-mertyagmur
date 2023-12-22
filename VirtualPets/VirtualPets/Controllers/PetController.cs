using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VirtualPets.Data;
using VirtualPets.DTOs;
using VirtualPets.Models;

namespace VirtualPets.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly VirtualPetDbContext context;
        private readonly IMapper mapper;
        private readonly IValidator<PetDTO> petValidator;

        public PetController(VirtualPetDbContext context, IMapper mapper, IValidator<PetDTO> petValidator)
        {
            this.context = context;
            this.mapper = mapper;
            this.petValidator = petValidator;
        }

        [HttpPost]
        public IActionResult CreatePet([FromBody] PetDTO petDTO)
        {
            var validationResult = petValidator.Validate(petDTO);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(error => error.ErrorMessage));
            }

            if (context.Users.Find(petDTO.UserId) == null)
            {
                return NotFound("User not found");
            }

            var pet = mapper.Map<Pet>(petDTO);

            context.Pets.Add(pet);
            context.SaveChanges();

            var responseDTO = mapper.Map<PetDTO>(pet);

            return CreatedAtAction(nameof(GetPet), new { petId = pet.Id }, responseDTO);
        }

        [HttpGet]
        public IActionResult ListPets()
        {
            var pets = context.Pets.ToList();

            var petDTOList = mapper.Map<List<PetDTO>>(pets);

            return Ok(petDTOList);
        }

        [HttpGet("{petId}")]
        public IActionResult GetPet(int petId)
        {
            var pet = context.Pets.Find(petId);
            if (pet == null)
            {
                return NotFound();
            }
            var petDTO = mapper.Map<PetDTO>(pet);

            return Ok(petDTO);
        }

        [HttpPut("{petId}")]
        public IActionResult UpdatePet(int petId, [FromBody] PetDTO updatedPetDTO)
        {
            var validationResult = petValidator.Validate(updatedPetDTO);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(error => error.ErrorMessage));
            }

            var pet = context.Pets.Find(petId);
            if (pet == null)
            {
                return NotFound();
            }

            mapper.Map(updatedPetDTO, pet);

            context.SaveChanges();

            var responseDTO = mapper.Map<PetDTO>(pet);

            return Ok(responseDTO);
        }
    }
}
