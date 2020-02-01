using System;
using System.Collections.Generic;
using MongoDB.Driver;
using TaskManager.Contracts.Models;

namespace TaskManager.Business.Services
{
    public class TicketService : ITicketService
    {
        private readonly IMongoCollection<Ticket> _tickets;
        public IColumnService ColumnService { get; set; }

        public TicketService(ITaskManagerStoreDatabaseSettings settings,
            IColumnService columnService)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _tickets = database.GetCollection<Ticket>(settings.TicketCollectionName);

            ColumnService = columnService;
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