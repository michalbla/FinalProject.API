using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TNAI_FinalProject.Dto.UserDto;
using TNAI_FinalProject.Model.Dtos.UserDto;
using TNAI_FinalProject.Repository.Users;

namespace TNAI_FinalProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        public AuthController(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        private bool IsValidUser(LogInInputUserDto inputUser)
        {
            var user = _userRepository.GetUserByEmailAsync(inputUser.Email);

            if (user == null) return false;

            return true;
        }
        private object GenerateJwtToken(LogInInputUserDto logInInputUserDto)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LogInInputUserDto logInInputUserDto)
        {
            if (logInInputUserDto == null)
            {
                return BadRequest("Invalid client request");
            }

            if (IsValidUser(logInInputUserDto)
            {
                var token = GenerateJwtToken(logInInputUserDto);
                return Ok(new { Token = token});
            }

            return BadRequest("Invalid credentials");
        }

       
    }
}
