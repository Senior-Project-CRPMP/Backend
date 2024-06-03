using Backend.Models.Project;

namespace Backend.Interfaces.Project
{
    public interface IProjectRepository
    {
        ICollection<Models.Project.Project> GetProjects();
        Models.Project.Project GetProject(int id);
        Models.Project.Project GetProject(string title);
        bool ProjectExists(int id);
        bool ProjectExists(string title);
        bool CreateProject(Models.Project.Project project);
        bool UpdateProject(Models.Project.Project project);
        bool DeleteProject(Models.Project.Project project);
        bool Save();
    }
}
