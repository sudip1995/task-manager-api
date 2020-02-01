using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TaskManager.Contracts.Models
{
    public class Board
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Title { get; set; }
    }
}