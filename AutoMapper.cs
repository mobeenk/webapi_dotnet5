using AutoMapper;
using webapi_dotnet5.DTOs;
using webapi_dotnet5.DTOs.Fights;
using webapi_dotnet5.Models;

namespace webapi_dotnet5
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            //source to destination
            CreateMap<Character, GetCharDto>();
            CreateMap<AddCharDto, Character>();
            CreateMap<Weapon, GetWeaponDto>();

            CreateMap<Skill, GetSkillDto>();
            CreateMap<Character, HighScoreDto>();
        }
    }
}