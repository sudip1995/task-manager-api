using TaskManager.Contracts.Models;

namespace TaskManager.Business.GraphQL
{
    public interface ITaskManagerDataMutator
    {
        Board AddBoard(Board board);
        Column AddColumn(Column column);
        Ticket AddTicket(Ticket ticket);
    }
}