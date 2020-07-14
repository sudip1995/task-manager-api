using System;
using System.Collections.Generic;
using TaskManager.Library.Database;

namespace TaskManager.Contracts.Models
{
    public class TicketDetails: ARepositoryItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<CheckList> CheckLists { get; set; }
        public List<Attachment> Attachments { get; set; }

        public TicketDetails()
        {
            Description = String.Empty;
            CheckLists = new List<CheckList>();
            Attachments = new List<Attachment>();
        }
    }
}