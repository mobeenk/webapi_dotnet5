using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using webapi_dotnet5.DTOs;
using webapi_dotnet5.Models;

namespace webapi_dotnet5.DAL
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }
          [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto userRegisterDto)
        {
            var response = await _authRepo.Register(
                new User { Username = userRegisterDto.Username }, userRegisterDto.Password
            );

            if(!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLoginDto userLoginDto)
        {
            var response = await _authRepo.Login(
                userLoginDto.Username, userLoginDto.Password
            );

            if(!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}