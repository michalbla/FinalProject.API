using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TNAI_FinalProject.Model.Entities;
using TNAI_FinalProject.Repository.Users;

namespace TNAI_FinalProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailsController : ControllerBase 
    {
        private readonly IUserDetailsRepository _userDetailsRepository;

        public UserDetailsController(IUserDetailsRepository userDetailsRepository)
        {
            _userDetailsRepository = userDetailsRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _userDetailsRepository.GetUserDetailsByIdAsync(id);

            if (user == null)
                return NotFound(new { Message = "Product not found" });

            return Ok(new
            {
                hasChilldren = user.HasChilldren,
                chillrenCount = user.ChilldrenCount,
                isHandicaped = user.IsHandicaped,
                age = user.Age,
                position = user.Position,
                payment = user.Payment,
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userDetailsRepository.GetAllUsersDetailsAsync();
            if (!users.Any()) return NotFound();
            var result = users.Select(user => new
            {
                hasChilldren = user.HasChilldren,
                chilldrenCount = user.ChilldrenCount,
                isHandicaped = user.IsHandicaped,
                age = user.Age,
                position = user.Position,
                payment = user.Payment,
            });

            return Ok(result);
        }
        //TODO: FORMULARZ ABY DODAC SZCZEGOLY UZYTKOWNIKA
/*        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDetails userDetails)
        {
            if (userDetails == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();

            var newUserDetails = new UserDetails()
            {

            }
        }*/
    }
}
