using Backend.Models;

namespace Backend.Interfaces
{
    public interface IProjectRepository
    {
        ICollection<Project> GetProjects();
        Project GetProject(int id);
        Project GetProject(string title);
        bool ProjectExists(int id);
        bool ProjectExists(string title);
        bool CreateProject(Project project);
        bool UpdateProject(Project project);    
        bool DeleteProject(Project project);
        bool Save();
    }
}
