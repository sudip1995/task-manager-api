using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Business.Services;
using TaskManager.Contracts.Models;

namespace TaskManager.Business.GraphQL
{
    public class TaskManagerDataProvider : ITaskManagerDataProvider
    {
        public IBoardService BoardService { get; set; }

        public TaskManagerDataProvider(IBoardService boardService)
        {
            BoardService = boardService;
        }
        public Task<List<Board>> GetBoards()
        {
            return BoardService.GetAll();
        }

        public Task<Board> GetBoard(string id)
        {
            return Task.FromResult(BoardService.Get(id));
        }
    }
}