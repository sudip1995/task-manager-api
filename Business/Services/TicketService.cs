using System;
using System.Collections.Generic;
using System.Composition;
using MongoDB.Driver;
using TaskManager.Contracts.Models;
using TaskManager.Library.Extensions;
using TaskManager.Library.Helpers;

namespace TaskManager.Business.Services
{
    [Export(typeof(ITicketService))]
    public class TicketService : ITicketService
    {
        private readonly IMongoCollection<Ticket> _tickets;
        [Import]
        public IColumnService ColumnService { get; set; }

        public TicketService()
        {
            var connectionString = ConfigurationHelper.Instance.GetDatabaseConnectionString();
            var client = new MongoClient(connectionString);
            var databaseName = ConfigurationHelper.Instance.GetDatabaseName();
            var database = client.GetDatabase(databaseName);

            _tickets = database.GetCollection<Ticket>($"{typeof(Ticket).Name}");
        }

        public List<Ticket> GetAll(string columnId)
        {
            return _tickets.Find(ticket => ticket.ColumnId == columnId).ToList();
        }

        public Ticket Add(Ticket ticket, string columnId)
        {
            if (string.IsNullOrWhiteSpace(ticket.Title))
            {
                throw new Exception("Cannot add a ticket without title");
            }
            var column = ColumnService.Get(columnId);
            if (column == null)
            {
                throw new Exception($"Column with id {columnId} doesn't exist");
            }

            var ticketCount = GetAll(columnId).Count;
            ticket.Order = ticketCount;

            _tickets.InsertOne(ticket);
            return ticket;
        }
    }
}