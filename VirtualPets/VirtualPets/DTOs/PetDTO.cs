﻿namespace VirtualPets.DTOs
{
    public class PetDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public string Breed { get; set; }
        public int? Age { get; set; }
    }
}
