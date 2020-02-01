using TaskManager.Contracts.Models;

namespace TaskManager.Business.GraphQL
{
    public interface ITaskManagerDataMutator
    {
        Board AddBoard(Board board);
    }
}