using Backend.Data;
using Backend.Interfaces.Project;
using Backend.Models.Project;
using Backend.Models.Account;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Repository.Project
{
    public class UserProjectRepository : IUserProjectRepository
    {
        private readonly DataContext _context;

        public UserProjectRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateUserProject(UserProject userProject)
        {
            _context.Add(userProject);
            return Save();
        }

        public bool DeleteUserProject(UserProject userProject)
        {
            _context.Remove(userProject);
            return Save();
        }

        public UserProject GetUserProject(int id)
        {
            return _context.UserProjects.Include(up => up.Project).Include(up => up.User).FirstOrDefault(up => up.Id == id);
        }

        public ICollection<UserProject> GetUserProjects()
        {
            return _context.UserProjects.Include(up => up.Project).Include(up => up.User).ToList();
        }

        public ICollection<Models.Project.Project> GetProjectsByUserId(string userId)
        {
            return _context.UserProjects.Include(up => up.Project)
                                        .Where(up => up.UserId == userId)
                                        .Select(up => up.Project)
                                        .ToList();
        }

        public ICollection<Models.Project.Project> GetProjectsByUserRole(string userId, string role)
        {
            return _context.UserProjects.Include(up => up.Project)
                                        .Where(up => up.UserId == userId && up.Role == role)
                                        .Select(up => up.Project)
                                        .ToList();
        }

        public ICollection<User> GetUsersByProjectId(int projectId)
        {
            return _context.UserProjects.Include(up => up.User)
                                        .Where(up => up.ProjectId == projectId)
                                        .Select(up => up.User)
                                        .ToList();
        }

        public bool UserProjectExists(int id)
        {
            return _context.UserProjects.Any(up => up.Id == id);
        }

        public bool UpdateUserProject(UserProject userProject)
        {
            _context.Update(userProject);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public ICollection<UserProject> GetUserProjectsByProjectId(int projectId)
        {
            return _context.UserProjects.Include(up => up.User)
                                        .Include(up => up.Project)
                                        .Where(up => up.ProjectId == projectId)
                                        .ToList();
        }
    }
}
