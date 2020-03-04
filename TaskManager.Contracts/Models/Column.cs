using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TaskManager.Library.Database;

namespace TaskManager.Contracts.Models
{
    public class Column
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string BoardId { get; set; }
        public List<Ticket> Tickets { get; set; }
        public Column()
        {
            Tickets = new List<Ticket>();
        }
        public Column(string title, string boardId)
        {
            Id = Guid.NewGuid().ToString();
            BoardId = boardId;
            Title = title;
            Tickets = new List<Ticket>();
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
