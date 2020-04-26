using System;
using System.Collections.Generic;

namespace TaskManager.Contracts.Models
{
    public class CheckList
    {
        public CheckList(string title)
        {
            Id = Guid.NewGuid().ToString();
            Title = title;
            CheckListItems = new List<CheckListItem>();
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public List<CheckListItem> CheckListItems { get; set; }
    }

    public class CheckListItem
    {
        public CheckListItem(string title)
        {
            Id = Guid.NewGuid().ToString();
            Title = title;
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public bool IsChecked { get; set; }
    }
}