using System.Collections.Generic;

namespace webapi_dotnet5.Models
{
    public class Character
    {
         public int Id { get; set; }
        public string Name { get; set; } = "Frodo";
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public RpgClass Class { get; set; } = RpgClass.Knight;
        //THis will generate the foreign key referecing user table
        public User User { get; set; }
        // One-to One relationship
        public Weapon Weapon { get; set; }
        //many-to-many

        public List<Skill> Skills { get; set; }
        public int Fights { get; set; }
        public int Victories { get; set; }
        public int Defeats { get; set; }
    }
}