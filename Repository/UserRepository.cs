using Backend.Data;
using Backend.Interfaces;
using Backend.Models;

namespace Backend.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public bool UserProjectExists(int id)
        {
            return _context.Users.Any(u => u.Id == id);
        }
        public bool CreateUser(UserModel user)
        {
            _context.Add(user);
            return Save();
        }

        public bool DeleteUser(UserModel user)
        {
            _context.Remove(user);
            return Save();
        }

        public UserModel GetUser(string username)
        {
            return _context.Users.Where(p => p.UserName == username).FirstOrDefault();
        }

        public ICollection<UserModel> GetUsersByProjectId(int projectId)
        {
            var users = _context.UserProjects
                .Where(up => up.ProjectId == projectId)
                .Select(up => up.User)
                .ToList();

            return users;
        }

        public UserModel GetUser(int id)
        {
            return _context.Users.Where(p => p.Id == id).FirstOrDefault();
        }

        public ICollection<UserModel> GetUsers()
        {
            return _context.Users.OrderBy(p => p.Id).ToList();
        }

        public bool UserExists(int id)
        {
            return _context.Users.Any(p => p.Id == id);
        }

        public bool UserExists(string username)
        {
            return _context.Users.Any(p => p.UserName == username);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateUser(UserModel user)
        {
            _context.Update(user);
            return Save();
        }
    }
}
