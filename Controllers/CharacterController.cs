using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi_dotnet5.DTOs;
using webapi_dotnet5.Models;
using webapi_dotnet5.Services;
using System.Security.Claims;
namespace webapi_dotnet5.Controllers
{
    [Authorize(Roles = "Player, Admin")]
    [ApiController]
    [Route("[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;
        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet("all")]

        public async Task<ActionResult<ServiceResponse<List<GetCharDto>>>> GetAll()
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier ).Value);
            return Ok(await _characterService.GetAllCharacters());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharDto>>> GetSingle(int id)
        {
            return Ok(await _characterService.GetCharacterById(id));
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetCharDto>>> AddCharacter(AddCharDto ch)
        { 
            return Ok(await _characterService.AddCharacter(ch) );
        }
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetCharDto>>> UpdateCharacter(UpdateCharDto updateCharDto){
            var response = await _characterService.UpdateCharacter(updateCharDto);
            if(response.Data == null){
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharDto>>> DeleteCharacter(int id){
            var response = await _characterService.DeleteCharacter(id);
            if(response.Data == null){
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpPost("Skill")]
        public async Task<ActionResult<ServiceResponse<GetCharDto>>> AddCharacterSkill(AddCharSkillDto newCharacterSkill)
        {
            return Ok(await _characterService.AddCharacterSkill(newCharacterSkill));
        }


    }
}