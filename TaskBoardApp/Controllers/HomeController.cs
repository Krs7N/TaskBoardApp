using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using TaskBoardApp.Core.Contracts;
using TaskBoardApp.Models.Home;

namespace TaskBoardApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITaskService _taskService;

        public HomeController(ILogger<HomeController> logger, ITaskService taskService)
        {
            _logger = logger;
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var taskBoards = (await _taskService.GetBoardsAsync())
                .Select(b => b.Name)
                .Distinct()
                .ToList();

            var tasksCount = new List<HomeBoardModel>();
            foreach (var boardName in taskBoards)
            {
                var tasksInBoard = (await _taskService.GetTasksWithBoardsAndUsersAsync()).Count(t => t.Board.Name == boardName);
                tasksCount.Add(new HomeBoardModel()
                {
                    BoardName = boardName,
                    TasksCount = tasksInBoard
                });
            }

            var userTasksCount = -1;

            if (User.Identity.IsAuthenticated)
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                userTasksCount = (await _taskService.GetTasksWithBoardsAndUsersAsync()).Count(t => t.OwnerId == currentUserId);
            }

            var model = new HomeViewModel()
            {
                AllTasksCount = (await _taskService.GetTasksWithBoardsAndUsersAsync()).Count(),
                BoardsWithTasksCount = tasksCount,
                UserTasksCount = userTasksCount
            };

            return View(model);
        }
    }
}