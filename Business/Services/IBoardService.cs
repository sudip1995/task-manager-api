using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Contracts.Models;

namespace TaskManager.Business.Services
{
    public interface IBoardService
    {
        Board Add(Board board);
        Board Get(string id);
        List<Board> GetAll();
        Board Update(string id, Board board);
    }
}