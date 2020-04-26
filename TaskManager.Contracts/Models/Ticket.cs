using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TaskManager.Library.Database;

namespace TaskManager.Contracts.Models
{
    public class Ticket
    {
        public string Id { get; set; }
        public string ColumnId { get; set; }
        public string Title { get; set; }
        public int CheckListCount { get; set; }
        public Ticket() { }
        public Ticket(string title, string columnId)
        {
            Id = ObjectId.GenerateNewId().ToString();
            ColumnId = columnId;
            Title = title;
        }
    }
}
