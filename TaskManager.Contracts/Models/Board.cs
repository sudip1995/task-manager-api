using TaskManager.Library.Database;

namespace TaskManager.Contracts.Models
{
    public class Board : ARepositoryItem
    {
        public string Title { get; set; }
    }
}