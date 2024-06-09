using Backend.Models.Project;
using System.Collections.Generic;

namespace Backend.Interfaces.Project
{
    public interface IProjectRepository
    {
        ICollection<Models.Project.Project> GetProjects();
        Models.Project.Project GetProject(int id);
        Models.Project.Project GetProject(string title);
        ICollection<Models.Project.Project> GetProjectsByTitleContains(string title);
        int GetProjectCount();
        bool ProjectExists(int id);
        bool ProjectExists(string title);
        bool CreateProject(Models.Project.Project project);
        bool CreateProjectWithUser(Models.Project.Project project, string userId);
        bool UpdateProject(Models.Project.Project project);
        bool DeleteProject(Models.Project.Project project);
        bool Save();
    }
}
