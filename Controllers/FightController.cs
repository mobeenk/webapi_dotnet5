using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi_dotnet5.DTOs.Fights;
using webapi_dotnet5.Models;
using webapi_dotnet5.Services;

namespace webapi_dotnet5.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class FightController : ControllerBase
    {
        private readonly IFightService _fightService;
        public FightController(IFightService fightService)
        {
            _fightService = fightService;

        }
        [HttpPost("Weapon")]
        public async Task<ActionResult<ServiceResponse<AttackResultDto>>> WeaponAttack(WeaponAttackDto weaponAttackDto)
        {

            return Ok(await _fightService.WeaponAttack(weaponAttackDto));
        }
         [HttpPost("Skill")]
        public async Task<ActionResult<ServiceResponse<SkillAttackDto>>> SkillAttack(SkillAttackDto skillAttackDto)
        {

            return Ok(await _fightService.SkillAttack(skillAttackDto));
        }
        [HttpPost("Fight")]
        public async Task<ActionResult<ServiceResponse<FightResultDto>>> Fight(FightRequestDto fightRequestDto)
        {

            return Ok(await _fightService.Fight(fightRequestDto));
        }
          [HttpGet]
        public async Task<ActionResult<ServiceResponse<HighScoreDto>>> GetHighscore()
        {
            return Ok(await _fightService.GetHighscore());
        }
    }
}