using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Business.Services;
using TaskManager.Contracts.Models;

namespace TaskManager.Business.GraphQL
{
    public class TaskManagerDataProvider : ITaskManagerDataProvider
    {
        public IBoardService BoardService { get; set; }
        public IColumnService ColumnService { get; set; }

        public TaskManagerDataProvider(IBoardService boardService,
            IColumnService columnService)
        {
            BoardService = boardService;
            ColumnService = columnService;
        }
        public Task<List<Board>> GetBoards()
        {
            return Task.FromResult(BoardService.GetAll());
        }

        public Task<Board> GetBoard(string id)
        {
            return Task.FromResult(BoardService.Get(id));
        }

        public Task<List<Column>> GetColumns(string boardId)
        {
            return Task.FromResult(ColumnService.GetAll(boardId));
        }
    }
}