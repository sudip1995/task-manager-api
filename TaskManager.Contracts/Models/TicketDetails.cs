using TaskManager.Library.Database;

namespace TaskManager.Contracts.Models
{
    public class TicketDetails: ARepositoryItem
    {
        public string Description { get; set; }
    }
}