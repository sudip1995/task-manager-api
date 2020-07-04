using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TaskManager.Library.Database;
using TaskManager.Library.Models;

namespace TaskManager.Contracts.Models
{
    public class Column
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string BoardId { get; set; }
        public List<Ticket> Tickets { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Column()
        {
            Tickets = new List<Ticket>();
        }
        public Column(string title, string boardId, User createdBy)
        {
            Id = Guid.NewGuid().ToString();
            CreatedBy = createdBy;
            BoardId = boardId;
            Title = title;
            Tickets = new List<Ticket>();
            CreatedDate = DateTime.UtcNow;
        }

        public void AddTicket(Ticket ticket)
        {
            Tickets.Add(ticket);
        }

        public void UpdateTicket(Ticket ticket)
        {
            var id = Tickets.FindIndex(o => o.Id == ticket.Id);
            if (id != -1)
            {
                Tickets[id] = ticket;
            }
        }
    }
}
