using System.Reflection;

namespace Backend.Interfaces.Project
{
    public interface ITaskRepository
    {
        ICollection<Models.Project.Task> GetTasks();
        ICollection<Models.Project.Task> GetProjectTasks(int id);
        int GetTaskCount();
        Models.Project.Task GetTask(int id);
        Models.Project.Task GetTask(string title);
        bool TaskExists(int id);
        bool TaskExists(string title);
        bool ProjectTaskExists(int id);
        bool CreateTask(Models.Project.Task task);
        bool UpdateTask(Models.Project.Task task);
        bool DeleteTask(Models.Project.Task task);
        bool Save();
    }
}
