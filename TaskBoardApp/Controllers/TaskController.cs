using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskBoardApp.Core.Contracts;
using TaskBoardApp.Models.Board;
using TaskBoardApp.Models.Task;
using Task = TaskBoardApp.Data.Models.Task;

namespace TaskBoardApp.Controllers;

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

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        var task = await _taskService.GetTaskDetailsByIdAsync(id);

        if (task == null)
        {
            return NotFound();
        }

        var model = new TaskDetailsViewModel()
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            CreatedOn = task.CreatedOn.ToString("dd/MM/yyyy HH:mm"),
            Board = task.Board.Name,
            Owner = task.Owner.UserName
        };

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        var task = await _taskService.GetTaskDetailsByIdAsync(id);

        if (task == null)
        {
            return NotFound();
        }

        string currentUserId = GetCurrentUserId();

        if (currentUserId != task.OwnerId)
        {
            return Unauthorized();
        }

        var model = new TaskFormModel()
        {
            Title = task.Title,
            Description = task.Description,
            BoardId = task.BoardId,
            Boards = (await GetBoardsAsync()).ToList()
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, TaskFormModel model)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        string currentUserId = GetCurrentUserId();

        var task = await _taskService.GetTaskDetailsByIdAsync(id);

        if (task == null)
        {
            return NotFound();
        }

        if (currentUserId != task.OwnerId)
        {
            return Unauthorized();
        }

        if ((await _taskService.GetBoardsAsync()).All(b => b.Id != model.BoardId))
        {
            ModelState.AddModelError(nameof(model.BoardId), "Board does not exist.");
        }

        if (!ModelState.IsValid)
        {
            model.Boards = (await GetBoardsAsync()).ToList();

            return View(model);
        }

        task.Title = model.Title;
        task.Description = model.Description;
        task.BoardId = model.BoardId;

        await _taskService.SaveChangesAsync();

        return RedirectToAction("All", "Board");
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        string currentUserId = GetCurrentUserId();

        var task = await _taskService.GetTaskDetailsByIdAsync(id);

        if (task == null)
        {
            return NotFound();
        }

        if (currentUserId != task.OwnerId)
        {
            return Unauthorized();
        }

        var model = new TaskViewModel()
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(TaskViewModel model)
    {
        string currentUserId = GetCurrentUserId();

        var task = await _taskService.GetTaskDetailsByIdAsync(model.Id);

        if (task == null)
        {
            return NotFound();
        }

        if (currentUserId != task.OwnerId)
        {
            return Unauthorized();
        }

        await _taskService.DeleteAsync(task);
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
