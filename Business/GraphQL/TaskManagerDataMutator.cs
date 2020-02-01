using TaskManager.Business.Services;
using TaskManager.Contracts.Models;

namespace TaskManager.Business.GraphQL
{
    public class TaskManagerDataMutator : ITaskManagerDataMutator
    {
        public IBoardService BoardService { get; set; }

        public TaskManagerDataMutator(IBoardService boardService)
        {
            BoardService = boardService;
        }
        public Board AddBoard(Board board)
        {
            return BoardService.Add(board);
        }
    }
}