using Backend.Models;
using System.Reflection;

namespace Backend.Interfaces
{
    public interface ITaskRepository
    {
        ICollection<Models.Task> GetTasks();
        ICollection<Models.Task> GetProjectTasks(int id);
        int GetTaskCount();
        Models.Task GetTask(int id);
        Models.Task GetTask(string title);
        bool TaskExists(int id);
        bool TaskExists(string title);
        bool ProjectTaskExists(int id);
        bool CreateTask(Models.Task task);
        bool UpdateTask(Models.Task task);
        bool DeleteTask(Models.Task task);
        bool Save();
    }
}
