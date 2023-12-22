using AutoMapper;
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

        public UserController(VirtualPetDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] UserDTO userDTO)
        {
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