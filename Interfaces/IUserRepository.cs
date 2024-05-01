using Backend.Models;

namespace Backend.Interfaces
{
    public interface IUserRepository
    {
        
            ICollection<UserModel> GetUsers();
            
            UserModel GetUser(int id);
            UserModel GetUser(string username);
            bool UserExists(int id);
            bool UserExists(string username);
            //bool UserExists(string email);
            bool CreateUser(UserModel user);
            bool UpdateUser(UserModel user);
            bool DeleteUser(UserModel user);
            bool Save();
        
    }

}
