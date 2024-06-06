using System.Collections.Generic;
using System.Linq;
using Backend.Data;
using Backend.Interfaces.Project;
using Backend.Models.Project;

namespace Backend.Repository.Project
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DataContext _context;

        public TaskRepository(DataContext context)
        {
            _context = context;
        }

        public bool ProjectTaskExists(int id)
        {
            return _context.Tasks.Any(t => t.Id == id);
        }

        public bool CreateTask(Models.Project.Task task)
        {
            _context.Add(task);
            return Save();
        }

        public bool DeleteTask(Models.Project.Task task)
        {
            _context.Remove(task);
            return Save();
        }

        public ICollection<Models.Project.Task> GetProjectTasks(int id)
        {
            return _context.Tasks.Where(t => t.ProjectId == id).ToList();
        }

        public Models.Project.Task GetTask(int id)
        {
            return _context.Tasks.Where(t => t.Id == id).FirstOrDefault();
        }

        public Models.Project.Task GetTask(string title)
        {
            return _context.Tasks.Where(t => t.Title == title).FirstOrDefault();
        }

        public int GetTaskCount()
        {
            return _context.Tasks.Count();
        }

        public ICollection<Models.Project.Task> GetTasks()
        {
            return _context.Tasks.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool TaskExists(int id)
        {
            return _context.Tasks.Any(t => t.Id == id);
        }

        public bool TaskExists(string title)
        {
            return _context.Tasks.Any(m => m.Title == title);
        }

        public bool UpdateTask(Models.Project.Task task)
        {
            _context.Update(task);
            return Save();
        }

        public ICollection<Models.Project.Task> GetUserTasks(string userId)
        {
            return _context.Tasks.Where(t => t.UserId == userId).ToList();
        }

        public ICollection<Models.Project.Task> GetTasksByProjectAndUser(int projectId, string userId)
        {
            return _context.Tasks.Where(t => t.ProjectId == projectId && t.UserId == userId).ToList();
        }
    }
}
