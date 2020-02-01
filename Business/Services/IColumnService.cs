using System.Collections.Generic;
using TaskManager.Contracts.Models;

namespace TaskManager.Business.Services
{
    public interface IColumnService
    {
        List<Column> GetAll(string boardId);
        Column Add(Column column);
        Column Get(string columnId);
    }
}