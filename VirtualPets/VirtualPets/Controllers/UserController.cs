using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VirtualPets.Data;
using VirtualPets.Models;

namespace VirtualPets.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly VirtualPetDbContext context;

        public UserController(VirtualPetDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
            return CreatedAtAction(nameof(GetUser), new { userId = user.Id }, user);
        }

        [HttpGet("{userId}")]
        public IActionResult GetUser(int userId)
        {
            var user = context.Users.Find(userId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}