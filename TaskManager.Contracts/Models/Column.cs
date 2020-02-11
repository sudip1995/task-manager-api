using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TaskManager.Library.Database;

namespace TaskManager.Contracts.Models
{
    public class Column : ARepositoryItem
    {
        public string Title { get; set; }
        public string BoardId { get; set; }
        public int Order { get; set; }

    }
}
