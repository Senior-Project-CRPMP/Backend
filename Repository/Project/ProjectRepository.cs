using Backend.Data;
using Backend.Interfaces.Project;
using Backend.Models.Project;

namespace Backend.Repository.Project
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DataContext _context;
        public ProjectRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateProject(Models.Project.Project project)
        {
            _context.Add(project);
            return Save();
        }

        public bool CreateProjectWithUser(Models.Project.Project project, string userId)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Add(project);
                Save();

                var userProject = new UserProject
                {
                    ProjectId = project.Id,
                    UserId = userId,
                    Role = "ProjectManager"
                };
                _context.Add(userProject);
                Save();

                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                return false;
            }
        }

        public bool DeleteProject(Models.Project.Project project)
        {
            _context.Remove(project);
            return Save();
        }

        public Models.Project.Project GetProject(string title)
        {
            return _context.Projects.Where(p => p.Title == title).FirstOrDefault();
        }

        public Models.Project.Project GetProject(int id)
        {
            return _context.Projects.Where(p => p.Id == id).FirstOrDefault();
        }

        public ICollection<Models.Project.Project> GetProjects()
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

        public bool UpdateProject(Models.Project.Project project)
        {
            _context.Update(project);
            return Save();
        }
    }
}
