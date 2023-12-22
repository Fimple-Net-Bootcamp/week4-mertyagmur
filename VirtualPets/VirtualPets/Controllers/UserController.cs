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
    public class UserController : ControllerBase
    {
        private readonly VirtualPetDbContext context;
        private readonly IMapper mapper;
        private readonly IValidator<UserDTO> userValidator;

        public UserController(VirtualPetDbContext context, IMapper mapper, IValidator<UserDTO> userValidator)
        {
            this.context = context;
            this.mapper = mapper;
            this.userValidator = userValidator;
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] UserDTO userDTO)
        {
            var validationResult = userValidator.Validate(userDTO);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(error => error.ErrorMessage));
            }

            var user = mapper.Map<User>(userDTO);

            context.Users.Add(user);
            context.SaveChanges();

            var responseDTO = mapper.Map<UserDTO>(user);

            return CreatedAtAction(nameof(GetUser), new { userId = user.Id }, responseDTO);
        }

        [HttpGet("{userId}")]
        public IActionResult GetUser(int userId)
        {
            var user = context.Users.Find(userId);
            if (user == null)
            {
                return NotFound();
            }
            var userDTO = mapper.Map<UserDTO>(user);

            return Ok(userDTO);
        }
    }
}