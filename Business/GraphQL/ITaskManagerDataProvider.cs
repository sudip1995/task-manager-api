using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Contracts.Models;

namespace TaskManager.Business.GraphQL
{
    public interface ITaskManagerDataProvider
    {
        Task<List<Board>> GetBoards();
        Task<Board> GetBoard(string id);
        Task<List<Column>> GetColumns(string boardId);
    }
}