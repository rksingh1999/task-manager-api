
using Microsoft.EntityFrameworkCore;
using TaskManager.API.Entities;
using TaskManagerApi.Data;

namespace TaskManager.API.Repositories.TaskRepository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TaskItem>> GetTasksList(Guid? userId = null)
        {
            return userId.HasValue
                ? await _context.Tasks
                    .Where(t => t.AssignedTo == userId)
                    .ToListAsync()
                : await _context.Tasks.ToListAsync();
        }

        public async Task<TaskItem?> GetTaskById(Guid id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task AddTask(TaskItem task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTask(TaskItem updatedTask)
        {
            var existingTask = await _context.Tasks.FindAsync(updatedTask.Id);

            if (existingTask == null)
                throw new Exception("Task not found.");

            existingTask.Title = updatedTask.Title;
            existingTask.Description = updatedTask.Description;
            existingTask.Status = updatedTask.Status;
            existingTask.DueDate = updatedTask.DueDate;
            existingTask.AssigneeName = updatedTask.AssigneeName;
            existingTask.AssignedTo = updatedTask.AssignedTo;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteTaskById(Guid id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }
    }
}
