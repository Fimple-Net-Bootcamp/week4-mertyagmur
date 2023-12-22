using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using VirtualPets.Models;

namespace VirtualPets.Data
{
    public class VirtualPetDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public VirtualPetDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("virtualpets");
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Pet>().ToTable("pets");
            modelBuilder.Entity<Health>().ToTable("health");
            modelBuilder.Entity<Food>().ToTable("foods");
            modelBuilder.Entity<Activity>().ToTable("activities");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Health> Health { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Food> Foods { get; set; }

        
    }
}
