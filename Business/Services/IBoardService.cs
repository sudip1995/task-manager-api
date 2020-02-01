using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Contracts.Models;

namespace TaskManager.Business.Services
{
    public interface IBoardService
    {
        void Add(Board board);
        Board Get(string id);
        Task<List<Board>> GetAll();
    }
}