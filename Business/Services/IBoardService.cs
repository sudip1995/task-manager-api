using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Contracts.Models;

namespace TaskManager.Business.Services
{
    public interface IBoardService
    {
        Board Add(string title);
        Board Get(string id);
        List<Board> GetAll();
        Board Update(string id, Board board);
        Board MoveColumn(string fromBoardId, string toBoardId, int previousIndex, int currentIndex);
    }
}