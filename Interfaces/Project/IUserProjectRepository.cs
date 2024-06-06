using Backend.Models.Account;
using Backend.Models.Project;
using System.Collections.Generic;

namespace Backend.Interfaces.Project
{
    public interface IUserProjectRepository
    {
        ICollection<UserProject> GetUserProjects();
        UserProject GetUserProject(int id);
        ICollection<Models.Project.Project> GetProjectsByUserId(string userId);
        ICollection<Models.Project.Project> GetProjectsByUserRole(string userId, string role);
        ICollection<User> GetUsersByProjectId(int projectId);
        ICollection<UserProject> GetUserProjectsByProjectId(int projectId);
        bool UserProjectExists(int id);
        bool CreateUserProject(UserProject userProject);
        bool UpdateUserProject(UserProject userProject);
        bool DeleteUserProject(UserProject userProject);
        bool Save();
    }
}
