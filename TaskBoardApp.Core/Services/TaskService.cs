using Microsoft.EntityFrameworkCore;
using TaskBoardApp.Core.Contracts;
using TaskBoardApp.Data;
using TaskBoardApp.Data.Models;
using Task = System.Threading.Tasks.Task;
using DataTask = TaskBoardApp.Data.Models.Task;

namespace TaskBoardApp.Core.Services;

public class TaskService : ITaskService
{
    private readonly TaskBoardAppDbContext _data;

    public TaskService(TaskBoardAppDbContext context)
    {
        _data = context;
    }

    public async Task<IEnumerable<Board>> GetBoardsAsync()
    {
        return await _data.Boards.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<DataTask>> GetTasksWithBoardsAndUsersAsync()
    {
        return await _data.Tasks.Include(t => t.Board).Include(t => t.Owner).AsNoTracking().ToListAsync();
    }

    public async Task AddAsync(DataTask task)
    {
        await _data.Tasks.AddAsync(task);
        await _data.SaveChangesAsync();
    }

    public async Task<DataTask?> GetTaskDetailsByIdAsync(int id)
    {
        return await _data.Tasks
            .Include(t => t.Owner)
            .Include(t => t.Board)
            .Where(t => t.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task DeleteAsync(DataTask task)
    {
        _data.Tasks.Remove(task);
        await _data.SaveChangesAsync();
    }

    public async Task SaveChangesAsync() => await _data.SaveChangesAsync();
}