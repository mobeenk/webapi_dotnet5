using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using webapi_dotnet5.DAL;
using webapi_dotnet5.DTOs;
using webapi_dotnet5.Models;

namespace webapi_dotnet5.Services
{
    public interface IWeaponService
    {
        Task<ServiceResponse<GetCharDto>> AddWeapon(AddWeaponDto weaponDto);
    }
    public class WeaponService : IWeaponService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public WeaponService(DataContext dataContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _dataContext = dataContext;

        }
        private int GetUserId() => 
                int.Parse(_httpContextAccessor.HttpContext.User
                                        .FindFirstValue(ClaimTypes.NameIdentifier));
        public async Task<ServiceResponse<GetCharDto>> AddWeapon(AddWeaponDto weaponDto)
        {
             var response = new ServiceResponse<GetCharDto>();
             try
             {
                 //get the character related to this logged in user
                 var character = await _dataContext.Characters
                    .FirstOrDefaultAsync(c => c.Id == weaponDto.CharacterId && c.User.Id == GetUserId());
                if ( character == null){
                    response.Success = false;
                    response.Message = "character not found";
                    return response;
                }
                //create a new weapon to be added to this character
                var weapon = new Weapon{
                    Name = weaponDto.Name,
                    Damage = weaponDto.Damage,
                    Character = character
                };
                //add the weapon to the weapons table
                _dataContext.Weapons.Add(weapon);
                await _dataContext.SaveChangesAsync();
                // here we need to map "Weapon" property inside character to => GetWeaponDto property in GetCharDto
                // so we set automapper
                response.Data = _mapper.Map<GetCharDto>(character);
             }
             catch(Exception ex){
                 response.Success = false;
                 response.Message = ex.Message;
             }                      
             return response;
        }
    }
}