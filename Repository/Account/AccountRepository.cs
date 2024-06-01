using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Backend.Interfaces.Account;
using Backend.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Repositories.Account
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AccountRepository(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<bool> AssignRoleToUserAsync(User user, string roleName)
        {
            if (await _roleManager.RoleExistsAsync(roleName))
            {
                var result = await _userManager.AddToRoleAsync(user, roleName);
                return result.Succeeded;
            }
            return false;
        }

        public async Task<bool> CreateRoleAsync(string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
                return roleResult.Succeeded;
            }
            return false;
        }

        public int GetAdminCount()
        {
            return _userManager.Users.Count(u => u.IsAdmin);
        }

        public int GetNonAdminCount()
        {
            return _userManager.Users.Count(u => !u.IsAdmin);
        }

        public User GetUserById(int id)
        {
            return _userManager.Users.FirstOrDefault(u => u.Id == id.ToString());
        }

        public User GetUserByName(string name)
        {
            return _userManager.Users.FirstOrDefault(u => u.FirstName == name || u.LastName == name);
        }

        public User GetUserByUserName(string username)
        {
            return _userManager.FindByNameAsync(username).Result;
        }

        public int GetUserCount()
        {
            return _userManager.Users.Count();
        }

        public ICollection<User> GetUsers()
        {
            return _userManager.Users.ToList();
        }

        public User GetUsersByEmail(string email)
        {
            return _userManager.FindByEmailAsync(email).Result;
        }

        public ICollection<User> GetUsersByRole(string role)
        {
            return _userManager.GetUsersInRoleAsync(role).Result;
        }

        public async Task<(SignInResult, string)> LoginUserAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return (SignInResult.Failed, null);
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user.UserName, password, false, false);
            if (!signInResult.Succeeded)
            {
                return (signInResult, null);
            }

            var token = GenerateJwtToken(user);
            return (signInResult, token);
        }



        private string GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id)
                }),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["DurationInMinutes"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        public async Task<IdentityResult> RegisterUserAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }
    }
}
