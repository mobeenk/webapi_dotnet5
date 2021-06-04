using Microsoft.EntityFrameworkCore;
using webapi_dotnet5.Models;

namespace webapi_dotnet5.DAL
{
    public class DataContext : DbContext
    {
        public DataContext( DbContextOptions options) : base(options)
        {

        }
        public DbSet<Character> Characters { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<Skill> Skills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<Skill>().HasData(
                new Skill {Id = 1 , Name= "FireBall" , Damage = 30},
                new Skill {Id = 2 , Name= "Frenzy" , Damage = 20},
                new Skill {Id = 3 , Name= "Blizzard" , Damage = 50}
            );
        }
    }
}