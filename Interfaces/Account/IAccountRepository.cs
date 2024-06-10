using System.Threading.Tasks;
using Backend.Models.Account;
using Microsoft.AspNetCore.Identity;

namespace Backend.Interfaces.Account
{
    public interface IAccountRepository
    {
        Task<bool> AssignRoleToUserAsync(User user, string roleName);
        Task<bool> RemoveRoleFromUserAsync(User user, string roleName);
        Task<bool> CreateRoleAsync(string roleName);
        int GetAdminCount();
        int GetStandardUserCount();
        User GetUserById(string id);
        User GetUserByName(string name);
        int GetUserCount();
        ICollection<User> GetUsers();
        User GetUsersByEmail(string email);
        ICollection<User> GetUsersByRole(string role);
        Task<(SignInResult, string, string)> LoginUserAsync(string email, string password); // Update return type
        Task<(string, string)> RefreshTokenAsync(string token, string refreshToken);
        Task<IdentityResult> RegisterUserAsync(User user, string password);
        Task<bool> DeleteUserAsync(User user);
        Task<IdentityResult> UpdateUserAsync(User user);
        ICollection<User> SearchUsers(string name);
    }
}
