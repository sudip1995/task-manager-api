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
        public Ticket() { }
        public Ticket(string title, string columnId)
        {
            Id = Guid.NewGuid().ToString();
            ColumnId = columnId;
            Title = title;
        }
    }
}
