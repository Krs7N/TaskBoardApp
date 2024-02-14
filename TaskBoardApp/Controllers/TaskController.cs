using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskBoardApp.Core.Contracts;
using TaskBoardApp.Models;
using Task = TaskBoardApp.Data.Models.Task;

namespace TaskBoardApp.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new TaskFormModel()
            {
                Boards = (await GetBoardsAsync()).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskFormModel model)
        {
            if ((await _taskService.GetBoardsAsync()).All(b => b.Id != model.BoardId))
            {
                ModelState.AddModelError(nameof(model.BoardId), "Board does not exist.");
            }

            string currentUserId = GetCurrentUserId();

            if (!ModelState.IsValid)
            {
                model.Boards = (await GetBoardsAsync()).ToList();

                return View(model);
            }

            var task = new Task()
            {
                Title = model.Title,
                BoardId = model.BoardId,
                CreatedOn = DateTime.Now,
                Description = model.Description,
                OwnerId = currentUserId
            };

            await _taskService.AddAsync(task);

            return RedirectToAction("All", "Board");
        }

        private string GetCurrentUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier);

        private async Task<IEnumerable<TaskBoardModel>> GetBoardsAsync()
        {
            return (await _taskService.GetBoardsAsync()).Select(b => new TaskBoardModel()
            {
                Id = b.Id,
                Name = b.Name
            });
        }
    }
}
