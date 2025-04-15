using TaskManager.API.Entities;

namespace TaskManager.API.Repositories.TaskRepository
{
    public interface ITaskRepository
    {
        Task<List<TaskItem>> GetTasksList(Guid? userId = null);
        Task<TaskItem?> GetTaskById(Guid id);
        Task AddTask(TaskItem task);
        Task UpdateTask(TaskItem task);
        Task DeleteTaskById(Guid id);
    }
}
