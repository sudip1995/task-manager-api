using TaskManager.Business.Services;
using TaskManager.Contracts.Models;

namespace TaskManager.Business.GraphQL
{
    public class TaskManagerDataMutator : ITaskManagerDataMutator
    {
        public IBoardService BoardService { get; set; }
        public IColumnService ColumnService { get; set; }
        public ITicketService TicketService { get; set; }

        public TaskManagerDataMutator(IBoardService boardService,
            IColumnService columnService,
            ITicketService ticketService)
        {
            BoardService = boardService;
            ColumnService = columnService;
            TicketService = ticketService;
        }
        public Board AddBoard(Board board)
        {
            return BoardService.Add(board);
        }

        public Column AddColumn(Column column)
        {
            return ColumnService.Add(column);
        }

        public Ticket AddTicket(Ticket ticket)
        {
            return TicketService.Add(ticket);
        }
    }
}