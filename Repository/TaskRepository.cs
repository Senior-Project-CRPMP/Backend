using Backend.Data;
using Backend.Interfaces;
using Backend.Models;

namespace Backend.Repository
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

        public bool CreateTask(TaskModel task)
        {
            _context.Add(task);
            return Save();
        }

        public bool DeleteTask(TaskModel task)
        {
            _context.Remove(task);
            return Save();
        }

        public ICollection<TaskModel> GetProjectTasks(int id)
        {
            return _context.Tasks.Where(t => t.ProjectId == id).ToList();
        }

        public TaskModel GetTask(int id)
        {
            return _context.Tasks.Where(t => t.Id == id).FirstOrDefault();
        }

        public TaskModel GetTask(string title)
        {
            return _context.Tasks.Where(t => t.Title == title).FirstOrDefault();
        }

        public int GetTaskCount()
        {
            return _context.Tasks.Count();
        }

        public ICollection<TaskModel> GetTasks()
        {
            return _context.Tasks.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool TaskExists(int id)
        {
            return _context.Tasks.Any(t => t.Id == id);
        }

        public bool TaskExists(string title)
        {
            return _context.Tasks.Any(m => m.Title == title);
        }

        public bool UpdateTask(TaskModel task)
        {
            _context.Update(task);
            return Save();
        }
    }
}
