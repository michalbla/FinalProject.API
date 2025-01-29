using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TNAI_FinalProject.Model.Dtos.UserDto;
using TNAI_FinalProject.Model.Entities;
using TNAI_FinalProject.Repository.Users;

namespace TNAI_FinalProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        public UserController(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);

            if (user == null)
                return NotFound(new { Message = "User not found" });

            return Ok(new
            {
                name = user.FirstName,
                lastName = user.LastName,
                email = user.Email,
                roleName = user.Role?.Name,
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepository.GetAllUsersAsync();
            if (!users.Any()) return NotFound();
            var result = users.Select(user => new
            {
                name = user.FirstName,
                lastName = user.LastName,
                email = user.Email,
                roleName = user.Role?.Name,
            });

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegisterInputUserDto user, [FromServices] IValidator<RegisterInputUserDto> validator)
        {
            if (user == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();

            var newUser = new User()
            {
                Email = user.Email,
                PasswordHash = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                RoleId = 3
            };

            var validationResult = await validator.ValidateAsync(user);
            if(!validationResult.IsValid) return BadRequest(validationResult.Errors);

            var hashedPassword = _passwordHasher.HashPassword(newUser, user.Password);
            newUser.PasswordHash = hashedPassword;

            var result = await _userRepository.SaveUserAsync(newUser);
            if (!result) throw new Exception("Error saving user");

            var savedUser = await _userRepository.GetUserByIdAsync(newUser.Id);

            var newUserDto = new RegisterUserDto
            {
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Email = newUser.Email,
                RoleName = newUser.Role?.Name,
            };

            return Ok(newUserDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] RegisterInputUserDto userDto, [FromServices] IValidator<RegisterInputUserDto> validator)
        {
            if (userDto == null) return BadRequest();

            if (!ModelState.IsValid) return BadRequest();

            var existingUser = await _userRepository.GetUserByIdAsync(id);

            if (existingUser == null) return NotFound();

            existingUser.FirstName = userDto.FirstName;
            existingUser.LastName = userDto.LastName;
            existingUser.Email = userDto.Email;
            existingUser.PasswordHash = userDto.Password;

            var validationResult = await validator.ValidateAsync(userDto);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            var hashedPassword = _passwordHasher.HashPassword(existingUser, userDto.Password);
            existingUser.PasswordHash = hashedPassword;

            var updatedUserDto = new RegisterUserDto
            {
                FirstName = existingUser.FirstName,
                LastName = existingUser.LastName,
                Email = existingUser.Email,
            };

            return Ok(updatedUserDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingUser = await _userRepository.GetUserByIdAsync(id);
            if (existingUser == null) return NotFound();

            var result = await _userRepository.DeleteUserAsync(id);
            if (!result) throw new Exception("Error deleting user");
            return Ok();
        }

        [HttpGet("by-email/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(email);

            if (existingUser == null) return NotFound(new { Message = "User not found" });

            return Ok(new
            {
                name = existingUser.FirstName,
                lastName = existingUser.LastName,
                email = existingUser.Email,
                roleName = existingUser.Role?.Name,
            });
        }
    }
}
