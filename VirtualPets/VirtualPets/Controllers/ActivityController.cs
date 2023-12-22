using Microsoft.AspNetCore.Mvc;
using System;
using VirtualPets.Data;
using VirtualPets.Models;

namespace VirtualPets.Controllers
{
    [ApiController]
    [Route("api/v1/activities")]
    public class ActivityController : ControllerBase
    {
        private readonly VirtualPetDbContext context;

        public ActivityController(VirtualPetDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public IActionResult AddActivity([FromBody] Activity activity)
        {
            context.Activities.Add(activity);
            context.SaveChanges();
            return CreatedAtAction(nameof(GetActivitiesForPet), new { petId = activity.Id }, activity);
        }

        [HttpGet("{petId}")]
        public IActionResult GetActivitiesForPet(int petId)
        {
            var pet = context.Pets.Find(petId);
            if (pet == null)
            {
                return NotFound("Pet not found");
            }

            var activities = context.Activities.ToList();
            return Ok(activities);
        }
    }
}
