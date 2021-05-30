using AutoMapper;
using webapi_dotnet5.DTOs;
using webapi_dotnet5.Models;

namespace webapi_dotnet5
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Character, GetCharDto>();
            CreateMap<AddCharDto, Character>();
        }
    }
}