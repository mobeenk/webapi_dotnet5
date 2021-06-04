using System;
using System.Collections.Generic;
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
    public interface ICharacterService
    {
        Task<ServiceResponse<List<GetCharDto>>> GetAllCharacters();
        Task<ServiceResponse<GetCharDto>> GetCharacterById(int id);
        Task<ServiceResponse<List<GetCharDto>>> AddCharacter(AddCharDto character);
        Task<ServiceResponse<GetCharDto>> UpdateCharacter(UpdateCharDto uc);
        Task<ServiceResponse<List<GetCharDto>>> DeleteCharacter(int id);
        Task<ServiceResponse<GetCharDto>> AddCharacterSkill(AddCharSkillDto charSkillDto);

    }
    public class CharacterService : ICharacterService
    {
        // private static List<Character> characters = new List<Character>{
        //     new Character {Id = 1 , Name = "Ali"},
        //     new Character {Id = 2,Name = "Ahamad"}
        // };
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CharacterService(IMapper mapper, DataContext dataContext, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _dataContext = dataContext;
            _mapper = mapper;

        }
        private int GetUserId() => 
                int.Parse(_httpContextAccessor.HttpContext.User
                                        .FindFirstValue(ClaimTypes.NameIdentifier));
        public async Task<ServiceResponse<List<GetCharDto>>> AddCharacter(AddCharDto newChar)
        {
            var serviceResponse = new ServiceResponse<List<GetCharDto>>();
            Character c = _mapper.Map<Character>(newChar);
            c.User = await _dataContext.Users
                .FirstOrDefaultAsync(i => i.Id == GetUserId());
            // add to database
            _dataContext.Characters.Add(c);
            //it will give the right sequental id
            await _dataContext.SaveChangesAsync();

            serviceResponse.Data = await _dataContext.Characters
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .Where(c=> c.User.Id == GetUserId())
                .Select(c => _mapper.Map<GetCharDto>(c)).ToListAsync();
            return serviceResponse;
        }



        public async Task<ServiceResponse<List<GetCharDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharDto>>();
            var dbChars = await _dataContext.Characters
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .Where(c => c.User.Id == GetUserId()).ToListAsync();
            serviceResponse.Data = dbChars.Select(x => _mapper.Map<GetCharDto>(x)).ToList();
            return serviceResponse;

        }

        public async Task<ServiceResponse<GetCharDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharDto>();
            var dbChar = await _dataContext.Characters
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .FirstOrDefaultAsync(x => x.Id == id  && x.User.Id == GetUserId());
            serviceResponse.Data = _mapper.Map<GetCharDto>(dbChar);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharDto>> UpdateCharacter(UpdateCharDto updateCharDto)
        {
            var serviceResponse = new ServiceResponse<GetCharDto>();
            try
            {
                Character c = await _dataContext.Characters
                    .Include(u => u.User)
                    .FirstOrDefaultAsync(x => x.Id == updateCharDto.Id );
                    
                if( c.User.Id == GetUserId()){
                    c.Name = updateCharDto.Name;
                    c.HitPoints = updateCharDto.HitPoints;
                    c.Strength = updateCharDto.Strength;
                    c.Defense = updateCharDto.Defense;
                    c.Intelligence = updateCharDto.Intelligence;
                    c.Class = updateCharDto.Class;

                    await _dataContext.SaveChangesAsync();
                    serviceResponse.Data = _mapper.Map<GetCharDto>(c);
                }
                else{
                    
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Character not found";
                
                }

            }
            catch (Exception ex)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharDto>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharDto>>();
            try
            {
                Character c = await _dataContext.Characters
                    .FirstOrDefaultAsync(x => x.Id == id && x.User.Id == GetUserId());
                if( c != null){
                    _dataContext.Characters.Remove(c);
                    await _dataContext.SaveChangesAsync();
                    serviceResponse.Data = _dataContext.Characters
                        .Where(u => u.User.Id == GetUserId())
                        .Select(x => _mapper.Map<GetCharDto>(x)).ToList();
                }
                else {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Character not found";
                }
                
            }
            catch (Exception ex)
            {
                serviceResponse.Data = null;
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharDto>> AddCharacterSkill(AddCharSkillDto charSkillDto)
        {
            var response = new ServiceResponse<GetCharDto>();
            
            try{
                var chaaracter = await _dataContext.Characters
                .Include(c => c.Weapon) // 1-1
                .Include(c => c.Skills) // 1-M 
                // if this sub table has another sub table we can fetch it with ThenIncude(...)
                .FirstOrDefaultAsync(
                    c => c.Id == charSkillDto.CharacterId && c.User.Id == GetUserId())
                ;
               if( chaaracter == null){
                    response.Success = false;
                    response.Message = "Character not found";
                    return response;
                }
                
                var skill = await _dataContext.Skills.FirstOrDefaultAsync(s => s.Id == charSkillDto.SkillId);

                if(skill == null){
                    response.Success = false;
                    response.Message = "Skill Not found";
                    return response;
                }

                chaaracter.Skills.Add(skill);
                await _dataContext.SaveChangesAsync();

                response.Data =  _mapper.Map<GetCharDto>(chaaracter);

                 
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}