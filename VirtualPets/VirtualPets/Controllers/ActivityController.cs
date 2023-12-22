using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using VirtualPets.Data;
using VirtualPets.DTOs;
using VirtualPets.Models;

namespace VirtualPets.Controllers
{
    [ApiController]
    [Route("api/v1/activities")]
    public class ActivityController : ControllerBase
    {
        private readonly VirtualPetDbContext context;
        private readonly IMapper mapper;

        public ActivityController(VirtualPetDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        public IActionResult AddActivity([FromBody] ActivityDTO activityDTO)
        {
            var activity = mapper.Map<Activity>(activityDTO);

            context.Activities.Add(activity);
            context.SaveChanges();

            var responseDTO = mapper.Map<ActivityDTO>(activity);

            return CreatedAtAction(nameof(GetActivitiesForPet), new { petId = activity.Id }, responseDTO);
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

            var activityDTOList = mapper.Map<List<ActivityDTO>>(activities);

            return Ok(activityDTOList);
        }
    }
}
