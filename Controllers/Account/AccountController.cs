using Backend.Interfaces.Account;
using Backend.Models.Account;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

        [HttpGet("admin-count")]
        public IActionResult GetAdminCount()
        {
            return Ok(_accountRepository.GetAdminCount());
        }

        [HttpGet("standard-user-count")]
        public IActionResult GetStandardUserCount()
        {
            return Ok(_accountRepository.GetStandardUserCount());
        }

        [HttpGet("user/{id}")]
        public IActionResult GetUserById(string id)
        {
            var user = _accountRepository.GetUserById(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpGet("user/name/{name}")]
        public IActionResult GetUserByName(string name)
        {
            var user = _accountRepository.GetUserByName(name);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpGet("user-count")]
        public IActionResult GetUserCount()
        {
            return Ok(_accountRepository.GetUserCount());
        }

        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            return Ok(_accountRepository.GetUsers());
        }

        [HttpGet("user/email/{email}")]
        public IActionResult GetUserByEmail(string email)
        {
            var user = _accountRepository.GetUsersByEmail(email);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpGet("users/role/{role}")]
        public IActionResult GetUsersByRole(string role)
        {
            var users = _accountRepository.GetUsersByRole(role);
            return Ok(users);
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

            var (signInResult, token, refreshToken) = await _accountRepository.LoginUserAsync(model.Email, model.Password);

            if (signInResult.Succeeded)
                return Ok(new { Token = token, RefreshToken = refreshToken });

            return BadRequest("Invalid login attempt");
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (newToken, newRefreshToken) = await _accountRepository.RefreshTokenAsync(model.Token, model.RefreshToken);

            if (newToken == null)
                return BadRequest("Invalid refresh token");

            return Ok(new { Token = newToken, RefreshToken = newRefreshToken });
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

        [HttpDelete("delete-user/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = _accountRepository.GetUserById(id);
            if (user == null)
                return NotFound("User not found");

            var result = await _accountRepository.DeleteUserAsync(user);
            if (result)
                return Ok("User deleted successfully");
            return BadRequest("Error deleting user");
        }

        [HttpPut("update-user/{id}")]
        public async Task<IActionResult> UpdateUser(string id, UpdateUserModel model)
        {
            var user = _accountRepository.GetUserById(id);
            if (user == null)
                return NotFound("User not found");

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.UserName = model.Email;
            user.IsAdmin = model.IsAdmin;

            var result = await _accountRepository.UpdateUserAsync(user);
            if (result.Succeeded)
                return Ok("User updated successfully");
            return BadRequest(result.Errors);
        }

    }

    public class AssignRoleModel
    {
        public string Email { get; set; }
        public string RoleName { get; set; }
    }

    public class RefreshTokenModel
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }

    public class UpdateUserModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
    }
}
