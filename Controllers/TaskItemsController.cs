using Microsoft.AspNetCore.Mvc;
using TaskManager.Data.Service;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    public class TaskItemsController : Controller
    {
        private readonly ITaskItemsService _taskItemsService;

        public TaskItemsController(ITaskItemsService taskItemsService)
        {
            _taskItemsService = taskItemsService;
        }

        public async Task<IActionResult> Index()
        {
            var taskItems = await _taskItemsService.GetAllAsync();
            return View(taskItems);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(TaskItem taskItem)
        {
            if (ModelState.IsValid)
            {
                taskItem.CreatedAt = DateTime.Now;
                await _taskItemsService.AddAsync(taskItem);
                return RedirectToAction("Index");
            }

            return View(taskItem);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var taskItem = (await _taskItemsService.GetAllAsync()).FirstOrDefault(t => t.Id == id);
            return View(taskItem);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TaskItem taskItem)
        {
            if (ModelState.IsValid)
            {
                await _taskItemsService.EditAsync(taskItem);
                return RedirectToAction("Index");
            }
            return View(taskItem);
        }

        public async Task<IActionResult> ToggleCompletion(int id)
        {
            await _taskItemsService.ToggleCompletion(id);
            return RedirectToAction("Index");
        }
        
        public async Task<IActionResult> DeleteTask(int id)
        {
            await _taskItemsService.DeleteTask(id);
            return RedirectToAction("Index");
        }
    }
}