using TaskBoardApp.Data.Models;
using Task = System.Threading.Tasks.Task;
using DataTask = TaskBoardApp.Data.Models.Task;

namespace TaskBoardApp.Core.Contracts;

public interface ITaskService
{
    Task<IEnumerable<Board>> GetBoardsAsync();

    Task AddAsync(DataTask task);

    Task<DataTask?> GetTaskDetailsByIdAsync(int id);
}