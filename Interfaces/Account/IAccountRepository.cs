using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models.Account;
using Microsoft.AspNetCore.Identity;

namespace Backend.Interfaces.Account
{
    public interface IAccountRepository
    {
        ICollection<User> GetUsers();
        ICollection<User> GetUsersByRole(string role);
        User GetUsersByEmail(string email);
        User GetUserByUserName(string username);
        User GetUserByName(string name);
        User GetUserById(int id);
        int GetUserCount();
        int GetAdminCount();
        int GetNonAdminCount();

        Task<IdentityResult> RegisterUserAsync(User user, string password);
        Task<(SignInResult, string)> LoginUserAsync(string email, string password);


        // Role management methods
        Task<bool> CreateRoleAsync(string roleName);
        Task<bool> AssignRoleToUserAsync(User user, string roleName);
    }
}
