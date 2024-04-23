using Backend.Models;

namespace Backend.Interfaces
{
    public interface IProjectRepository
    {
        ICollection<ProjectModel> GetProjects();
        ProjectModel GetProject(int id);
        ProjectModel GetProject(string title);
        bool ProjectExists(int id);
        bool ProjectExists(string title);
        bool CreateProject(ProjectModel project);
        bool UpdateProject(ProjectModel project);    
        bool DeleteProject(ProjectModel project);
        bool Save();
    }
}
