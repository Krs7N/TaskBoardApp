using Microsoft.EntityFrameworkCore;
using TaskBoardApp.Core.Contracts;
using TaskBoardApp.Data;
using TaskBoardApp.Data.Models;

namespace TaskBoardApp.Core.Services;

public class BoardService : IBoardService
{
    private readonly TaskBoardAppDbContext _context;

    public BoardService(TaskBoardAppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Board>> GetAllAsync()
    {
        return await _context.Boards.Include(b => b.Tasks).AsNoTracking().ToListAsync();
    }
}