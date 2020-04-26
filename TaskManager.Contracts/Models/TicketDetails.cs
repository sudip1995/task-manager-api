using System.Collections.Generic;
using TaskManager.Library.Database;

namespace TaskManager.Contracts.Models
{
    public class TicketDetails: ARepositoryItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<CheckList> CheckLists { get; set; }

        public TicketDetails()
        {
            CheckLists = new List<CheckList>();
        }
        public void Add(CheckList checkList)
        {
            CheckLists.Add(checkList);
        }
    }
}