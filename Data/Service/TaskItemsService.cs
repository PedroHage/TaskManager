using Microsoft.EntityFrameworkCore;
using TaskManager.Models;

namespace TaskManager.Data.Service
{
    public class TaskItemsService : ITaskItemsService
    {
        private readonly TaskManagerContext _context;
        public TaskItemsService(TaskManagerContext context)
        {
            _context = context;
        }
        public async Task AddAsync(TaskItem taskItem)
        {
            _context.TaskItems.Add(taskItem);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(TaskItem taskItem)
        {
            var existingTask = await GetTaskAsync(taskItem.Id);
            existingTask.Title = taskItem.Title;
            existingTask.Description = taskItem.Description;
            existingTask.DueDate = taskItem.DueDate;
            existingTask.IsCompleted = taskItem.IsCompleted;
            existingTask.CompletedAt = taskItem.IsCompleted ? DateTime.Now : null;
            await _context.SaveChangesAsync();
            
        }

        public async Task DeleteTask(int id)
        {
            var taskItem =  await GetTaskAsync(id);
            _context.Remove(taskItem);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            var tasktItems = await _context.TaskItems.ToListAsync();
            return tasktItems;
        }

        public async Task<TaskItem> GetTaskAsync(int id)
        {
            var task = await _context.TaskItems.FindAsync(id);
            if (task == null)
                throw new KeyNotFoundException("Task item not found.");
            else
                return task;
        }

        public async Task ToggleCompletion(int id)
        {
            var task = await GetTaskAsync(id);
            if (task.IsCompleted)
            {
                task.IsCompleted = false;
                task.CompletedAt = null;
            }
            else
            {
                task.IsCompleted = true;
                task.CompletedAt = DateTime.Now;
            }
            await _context.SaveChangesAsync();
        }
    }
}
