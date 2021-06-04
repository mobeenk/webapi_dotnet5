using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi_dotnet5.DAL;
using webapi_dotnet5.DTOs;
using webapi_dotnet5.Models;
using webapi_dotnet5.Services;

namespace webapi_dotnet5.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeaponController : ControllerBase
    {
        private readonly IWeaponService _weaponService;
        public WeaponController(IWeaponService weaponService)
        {
            _weaponService = weaponService;

        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetCharDto>>> AddWeapon(AddWeaponDto weaponDto){
            
            return Ok(await _weaponService.AddWeapon(weaponDto));
        }
    }
}