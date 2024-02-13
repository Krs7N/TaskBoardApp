using TaskBoardApp.Data.Models;

namespace TaskBoardApp.Core.Contracts;

public interface IBoardService
{
    Task<IEnumerable<Board>> GetAllAsync();
}