using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TaskManager.Library.Database;
using TaskManager.Library.Models;

namespace TaskManager.Contracts.Models
{
    public class Ticket
    {
        public string Id { get; set; }
        public string ColumnId { get; set; }
        public string Title { get; set; }
        public int CheckListCount { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Ticket() { }
        public Ticket(string title, string columnId, User createdBy)
        {
            Id = ObjectId.GenerateNewId().ToString();
            ColumnId = columnId;
            Title = title;
            CreatedBy = createdBy;
            CreatedDate = DateTime.UtcNow;
        }
    }
}
