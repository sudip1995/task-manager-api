using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Business.Services;
using TaskManager.Contracts.Models;
using TaskManager.Library.Ioc;

namespace TaskManager.Business.GraphQL
{
    public class TaskManagerDataProvider : ITaskManagerDataProvider
    {
        public IBoardService BoardService { get; set; }
        public IColumnService ColumnService { get; set; }
        public ITicketService TicketService { get; set; }

        public TaskManagerDataProvider()
        {
            BoardService = IocContainer.Instance.Resolve<IBoardService>();
            ColumnService = IocContainer.Instance.Resolve<IColumnService>();
            TicketService = IocContainer.Instance.Resolve<ITicketService>();
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

        public Task<List<Ticket>> GetTickets(string columnId)
        {
            return Task.FromResult(TicketService.GetAll(columnId));
        }
    }
}