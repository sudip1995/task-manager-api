using TaskManager.Contracts.Models;

namespace TaskManager.Business.GraphQL
{
    public interface ITaskManagerDataMutator
    {
        Board AddBoard(Board board);
        Column AddColumn(Column column, string boardId);
        Ticket AddTicket(Ticket ticket, string columnId);
    }
}