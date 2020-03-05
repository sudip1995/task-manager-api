using System.Collections.Generic;
using TaskManager.Contracts.Models;

namespace TaskManager.Business.Services
{
    public interface IColumnService
    {
        List<Column> GetAll(string boardId);
        Column Add(string title, string boardId);
        Column Get(string id);
        Column Update(string id, Column column);
        Board MoveTicket(string fromBoardId, string toBoardId, string fromColumnId, string toColumnId, int previousIndex, int currentIndex);
    }
}