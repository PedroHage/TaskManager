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
            var existingTask = (await GetAllAsync()).FirstOrDefault(t => t.Id == taskItem.Id);
            if (existingTask != null)
            {
                existingTask.Title = taskItem.Title;
                existingTask.Description = taskItem.Description;
                existingTask.DueDate = taskItem.DueDate;
                existingTask.IsCompleted = taskItem.IsCompleted;
                existingTask.CompletedAt = taskItem.IsCompleted ? DateTime.Now : null;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Task item not found.");
            }
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            var tasktItems = await _context.TaskItems.ToListAsync();
            return tasktItems;
        }
    }
}
