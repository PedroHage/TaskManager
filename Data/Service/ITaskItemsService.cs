using TaskManager.Models;

namespace TaskManager.Data.Service
{
    public interface ITaskItemsService
    {
        Task<IEnumerable<TaskItem>> GetAllAsync();
        Task AddAsync(TaskItem taskItem);
        Task EditAsync(TaskItem taskItem);
        Task<TaskItem> GetTaskAsync(int id);
        Task ToggleCompletion(int id);
    }
}
