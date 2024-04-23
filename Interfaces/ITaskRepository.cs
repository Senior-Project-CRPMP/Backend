using Backend.Models;
using System.Reflection;

namespace Backend.Interfaces
{
    public interface ITaskRepository
    {
        ICollection<TaskModel> GetTasks();
        ICollection<TaskModel> GetProjectTasks(int id);
        int GetTaskCount();
        TaskModel GetTask(int id);
        TaskModel GetTask(string title);
        bool TaskExists(int id);
        bool TaskExists(string title);
        bool ProjectTaskExists(int id);
        bool CreateTask(TaskModel task);
        bool UpdateTask(TaskModel task);
        bool DeleteTask(TaskModel task);
        bool Save();
    }
}
