using Microsoft.EntityFrameworkCore;
using TaskManager.Models;

namespace TaskManager.Data
{
    public class TaskManagerContext : DbContext
    {
        public DbSet<TaskItem> TaskItems { get; set; }

        public TaskManagerContext(DbContextOptions options) : base(options)
        {
        }
    }
}
