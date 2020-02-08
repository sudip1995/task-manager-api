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

        public Ticket Add(Ticket ticket)
        {
            var column = ColumnService.Get(ticket.ColumnId);
            if (column == null)
            {
                throw new Exception($"Column with id {ticket.ColumnId} doesn't exist");
            }

            var ticketCount = GetAll(ticket.ColumnId).Count;
            ticket.Order = ticketCount;

            _tickets.InsertOne(ticket);
            return ticket;
        }
    }
}