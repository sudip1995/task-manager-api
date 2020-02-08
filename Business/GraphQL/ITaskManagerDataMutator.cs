using TaskManager.Contracts.Models;

namespace TaskManager.Business.GraphQL
{
    public interface ITaskManagerDataMutator
    {
        Board AddBoard(Board board);
        Column AddColumn(Column column, string boardId);
        Ticket AddTicket(Ticket ticket, string columnId);
        Board UpdateBoard(string id, Board board);
        Column UpdateColumn(string id, Column column);
        Ticket UpdateTicket(string id, Ticket ticket);
    }
}