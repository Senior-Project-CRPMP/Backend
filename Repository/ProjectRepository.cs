using Backend.Data;
using Backend.Interfaces;
using Backend.Models;

namespace Backend.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DataContext _context;
        public ProjectRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateProject(ProjectModel project)
        {
            _context.Add(project);
            return Save();
        }

        public bool DeleteProject(ProjectModel project)
        {
            _context.Remove(project);
            return Save();
        }

        public ProjectModel GetProject(string title)
        {
            return _context.Projects.Where(p => p.Title == title).FirstOrDefault();
        }

        public ProjectModel GetProject(int id)
        {
            return _context.Projects.Where(p => p.Id == id).FirstOrDefault();
        }

        public ICollection<ProjectModel> GetProjects()
        {
            return _context.Projects.OrderBy(p => p.Id).ToList();
        }

        public bool ProjectExists(int id)
        {
            return _context.Projects.Any(p => p.Id == id);
        }

        public bool ProjectExists(string title)
        {
            return _context.Projects.Any(p => p.Title == title);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateProject(ProjectModel project)
        {
            _context.Update(project);
            return Save();
        }
    }
}
