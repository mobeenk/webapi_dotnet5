using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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

    }
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character>{
            new Character {Id = 1 , Name = "Ali"},
            new Character {Id = 2,Name = "Ahamad"}
        };
        private readonly IMapper _mapper;
        public CharacterService(IMapper mapper)
        {
            _mapper = mapper;

        }

        public async Task<ServiceResponse<List<GetCharDto>>> AddCharacter(AddCharDto newChar)
        {
            var serviceResponse = new ServiceResponse<List<GetCharDto>>();
            Character c = _mapper.Map<Character>(newChar);
            c.Id = characters.Max(i => i.Id)+1;
            characters.Add(c);
            serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharDto>(c)).ToList();
            return serviceResponse;
        }

      

        public async Task<ServiceResponse<List<GetCharDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharDto>>();
            serviceResponse.Data = characters.Select(x => _mapper.Map<GetCharDto>(x)).ToList();
            return serviceResponse;

        }

        public async Task<ServiceResponse<GetCharDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharDto>();
            serviceResponse.Data = _mapper.Map<GetCharDto>(  characters.FirstOrDefault(x => x.Id == id));
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharDto>> UpdateCharacter(UpdateCharDto updateCharDto)
        {
            var serviceResponse = new ServiceResponse<GetCharDto>();
            try{
                Character c = characters.FirstOrDefault(x => x.Id == updateCharDto.Id);
                c.Name = updateCharDto.Name;
                c.HitPoints = updateCharDto.HitPoints;
                c.Strength = updateCharDto.Strength;
                c.Defense = updateCharDto.Defense;
                c.Intelligence = updateCharDto.Intelligence;
                c.Class = updateCharDto.Class;
                serviceResponse.Data = _mapper.Map<GetCharDto>(c);

            }
            catch(Exception ex){
                serviceResponse.Data = null;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<GetCharDto>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharDto>>();
            try{
                Character c = characters.First(x => x.Id == id);
                characters.Remove(c);
                serviceResponse.Data = characters.Select(x => _mapper.Map<GetCharDto>(x)).ToList();
            }
            catch(Exception ex){
                serviceResponse.Data = null;
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

    }
}