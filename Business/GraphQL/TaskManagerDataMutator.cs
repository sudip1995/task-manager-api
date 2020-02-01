using TaskManager.Business.Services;
using TaskManager.Contracts.Models;

namespace TaskManager.Business.GraphQL
{
    public class TaskManagerDataMutator : ITaskManagerDataMutator
    {
        public IBoardService BoardService { get; set; }
        public IColumnService ColumnService { get; set; }

        public TaskManagerDataMutator(IBoardService boardService,
            IColumnService columnService)
        {
            BoardService = boardService;
            ColumnService = columnService;
        }
        public Board AddBoard(Board board)
        {
            return BoardService.Add(board);
        }

        public Column AddColumn(Column column)
        {
            return ColumnService.Add(column);
        }
    }
}