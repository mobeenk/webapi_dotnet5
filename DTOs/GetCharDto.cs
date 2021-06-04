using System.Collections.Generic;
using webapi_dotnet5.Models;

namespace webapi_dotnet5.DTOs
{
    public class GetCharDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Frodo";
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public RpgClass Class { get; set; } = RpgClass.Knight;
        //this is added to return weapon details on WeaponController Request
        
        public GetWeaponDto Weapon { get; set; }
        // char has many skills
        public List<GetSkillDto> Skills { get; set; }

        public int Fights { get; set; }
        public int Victories { get; set; }
        public int Defeats { get; set; }
    }
}