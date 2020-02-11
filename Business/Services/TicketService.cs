using System;
using System.Collections.Generic;
using System.Composition;
using GraphQL;
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
        public Ticket Get(string id)
        {
            return _tickets.Find(ticket => ticket.Id == id).FirstOrDefault();
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
            ticket.ColumnId = columnId;
            _tickets.InsertOne(ticket);
            return ticket;
        }

        public Ticket Update(string id, Ticket ticket)
        {
            var currentTicket = Get(id);
            if (currentTicket == null)
            {
                throw new Exception($"Can't find any ticket with id {id}");
            }
            var filter = Builders<Ticket>.Filter.Eq(o => o.Id, id);
            var updatedTicket = new Ticket();

            foreach (var propertyInfo in ticket.GetType().GetProperties())
            {
                updatedTicket.GetType().GetProperty(propertyInfo.Name)?.SetValue(updatedTicket, ticket.GetPropertyValue(propertyInfo.Name) ?? currentTicket.GetPropertyValue(propertyInfo.Name));
            }

            _tickets.ReplaceOne(filter, updatedTicket);
            return currentTicket;
        }
    }
}