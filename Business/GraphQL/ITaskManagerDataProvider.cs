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
        Task<List<Ticket>> GetTickets(string columnId);
        Task<TicketDetails> GetTicketDetails(string id);
        Task<List<CheckList>> GetCheckLists(string ticketId);
    }
}