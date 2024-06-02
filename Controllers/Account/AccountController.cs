using Backend.Interfaces.Account;
using Backend.Models.Account;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(Register model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,
                IsAdmin = false
            };

            var result = await _accountRepository.RegisterUserAsync(user, model.Password);

            if (result.Succeeded)
                return Ok("Registration made successfully");

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(Login model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (signInResult, token) = await _accountRepository.LoginUserAsync(model.Email, model.Password);

            if (signInResult.Succeeded)
                return Ok(new { Token = token });

            return BadRequest("Invalid login attempt");
        }

        [HttpPost("create-role")]
        public async Task<IActionResult> CreateRole([FromBody] string roleName)
        {
            var result = await _accountRepository.CreateRoleAsync(roleName);
            if (result)
                return Ok("Role created successfully");
            return BadRequest("Error creating role");
        }

        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRoleToUser([FromBody] AssignRoleModel model)
        {
            var user = _accountRepository.GetUsersByEmail(model.Email);
            if (user == null)
                return NotFound("User not found");

            var result = await _accountRepository.AssignRoleToUserAsync(user, model.RoleName);
            if (result)
                return Ok("Role assigned successfully");
            return BadRequest("Error assigning role");
        }

        [HttpPost("remove-role")]
        public async Task<IActionResult> RemoveRoleFromUser([FromBody] AssignRoleModel model)
        {
            var user = _accountRepository.GetUsersByEmail(model.Email);
            if (user == null)
                return NotFound("User not found");

            var result = await _accountRepository.RemoveRoleFromUserAsync(user, model.RoleName);
            if (result)
                return Ok("Role removed successfully");
            return BadRequest("Error removing role");
        }
    }

    public class AssignRoleModel
    {
        public string Email { get; set; }
        public string RoleName { get; set; }
    }
}
