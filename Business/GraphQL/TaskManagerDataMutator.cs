using TaskManager.Business.Services;
using TaskManager.Contracts.Models;
using TaskManager.Library.Ioc;

namespace TaskManager.Business.GraphQL
{
    public class TaskManagerDataMutator : ITaskManagerDataMutator
    {
        public IBoardService BoardService { get; set; }
        public IColumnService ColumnService { get; set; }
        public ITicketService TicketService { get; set; }

        public TaskManagerDataMutator()
        {
            BoardService = IocContainer.Instance.Resolve<IBoardService>();
            ColumnService = IocContainer.Instance.Resolve<IColumnService>();
            TicketService = IocContainer.Instance.Resolve<ITicketService>();
        }
        public Board AddBoard(Board board)
        {
            return BoardService.Add(board);
        }

        public Column AddColumn(Column column, string boardId)
        {
            return ColumnService.Add(column, boardId);
        }

        public Ticket AddTicket(Ticket ticket, string columnId)
        {
            return TicketService.Add(ticket, columnId);
        }
    }
}