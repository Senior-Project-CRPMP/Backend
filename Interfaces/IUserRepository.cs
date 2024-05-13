using Backend.Models;

namespace Backend.Interfaces
{
    public interface IUserRepository
    {
        
            ICollection<UserModel> GetUsers();
            ICollection<UserModel> GetUsersByProjectId(int id);
            UserModel GetUser(int id);
            UserModel GetUser(string username);
            bool UserExists(int id);
            bool UserExists(string username);
            bool UserProjectExists(int projectId);
            bool CreateUser(UserModel user);
            bool UpdateUser(UserModel user);
            bool DeleteUser(UserModel user);
            bool Save();
        
    }

}
