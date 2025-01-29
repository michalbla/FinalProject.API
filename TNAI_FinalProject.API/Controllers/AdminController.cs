using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TNAI_FinalProject.Repository.Users;
using TNAI_FinalProject.Model.Entities;
using TNAI_FinalProject.Repository.Admins;
using FluentValidation;
using TNAI_FinalProject.Model.Dtos.UserDto;
using TNAI_FinalProject.Dto.AdminDto;
//using TNAI_FinalProject.API.Migrations;
using Microsoft.EntityFrameworkCore;
using TNAI_FinalProject.Model;


namespace TNAI_FinalProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IUserRepository _userRepository;
        //private readonly IPasswordHasher<Model.Entities.Admin> _passwordHasher;
        public AdminController(IAdminRepository adminRepository, IUserRepository userRepository)//, IPasswordHasher<Model.Entities.Admin> passwordHasher)
        {
            _adminRepository = adminRepository;
            _userRepository = userRepository;
            // _passwordHasher = passwordHasher;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var admin = await _adminRepository.GetAdminByIdAsync(id);

            if (admin == null)
                return NotFound(new { Message = "User not found" });

            return Ok(new
            {
                id = admin.Id,
                Name = admin.Name
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var admins = await _adminRepository.GetAllAdminsAsync();
            if (!admins.Any()) return NotFound();

            var result = admins.Select(admin => new
            {
                id = admin.Id,
                Name = admin.Name
            });

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegisterInputAdminDto admin) //, [FromServices] IValidator<RegisterInputAdminDto> validator)
        {
            if (admin == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();

            var newAdmin = new Model.Entities.Admin()
            {
                Name = admin.Name,
                PasswordHash = admin.Password,

            };


            //Validator dla Admina do rozważenia
            // var validationResult = await validator.ValidateAsync(user);
            // if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            //  var hashedPassword = _passwordHasher.HashPassword(newUser, user.Password);
            // newUser.PasswordHash = hashedPassword;

            var result = await _adminRepository.SaveAdminAsync(newAdmin);
            if (!result) throw new Exception("Error saving administrator");

            //   var savedAdmin = await _adminRepository.GetAdminByIdAsync(newAdmin.Id);

            var newAdminDto = new RegisterAdminDto
            {
                Id = newAdmin.Id,
                Name = admin.Name,
            };

            return Ok(newAdminDto);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] RegisterInputAdminDto adminDto)//, [FromServices] IValidator<RegisterInputUserDto> validator)
        {
            if (adminDto == null) return BadRequest();

            if (!ModelState.IsValid) return BadRequest();

            var existingAdmin = await _adminRepository.GetAdminByIdAsync(id);

            if (existingAdmin == null) return NotFound();

            existingAdmin.Name = adminDto.Name;
            existingAdmin.PasswordHash = adminDto.Password;


            /*
            var validationResult = await validator.ValidateAsync(userDto);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            var hashedPassword = _passwordHasher.HashPassword(existingUser, userDto.Password);
            existingUser.PasswordHash = hashedPassword;

            */

            var updatedAdminDto = new RegisterAdminDto
            {
                Name = existingAdmin.Name,
                Id = existingAdmin.Id,
            };

            return Ok(updatedAdminDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingAdmin = await _adminRepository.GetAdminByIdAsync(id);
            if (existingAdmin == null) return NotFound();

            var result = await _adminRepository.DeleteAdminAsync(id);
            if (!result) throw new Exception("Error deleting administrator");
            return Ok();
        }

        [HttpGet("{id}/{email}")]
        public async Task<IActionResult> CheckUserAdmin(int id, string email)
        {
            var existingAdmin = await _adminRepository.GetAdminByIdAsync(id);

            if (existingAdmin == null) return NotFound();

            var existingUser = await _userRepository.GetUserByEmailAsync(email);

            if (existingUser == null) return NotFound();

            bool exist = await _adminRepository.CheckUserAdminAsync(id, existingUser);

            if (exist) throw new Exception("User is not connect with Administrator");
            return Ok();
        }

        [HttpPost("{id}/add-user-to-admin")]
        public async Task<IActionResult> AddUserToAdmin(int id, string email)
        {
            var existingAdmin = await _adminRepository.GetAdminByIdAsync(id);

            if (existingAdmin == null) return NotFound("Admin does not exist");

            var existingUser = await _userRepository.GetUserByEmailAsync(email);

            if (existingUser == null) return NotFound();

            bool result = await _adminRepository.AddUserToAdminAsync(id, existingUser);

            if (!result) throw new Exception("Error");
            return Ok(existingUser);
        }

        [HttpDelete("{id}/remove-user-from-admin")]
        public async Task<IActionResult> RemoveUserFAdmin(int id, string email)
        {
            var existingAdmin = await _adminRepository.GetAdminByIdAsync(id);

            if (existingAdmin == null) return NotFound("Admin does not exist");

            var existingUser = await _userRepository.GetUserByEmailAsync(email);

            if (existingUser == null) return NotFound();

            bool result = await _adminRepository.RemoveUserFromAdminAsync(id, existingUser);

            if (!result) throw new Exception("Error");
            return Ok();
        }

    }
}
