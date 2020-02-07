using System;
using System.Collections.Generic;
using MongoDB.Driver;
using TaskManager.Contracts.Models;
using TaskManager.Library;

namespace TaskManager.Business.Services
{
    public class TicketService : ITicketService
    {
        private readonly IMongoCollection<Ticket> _tickets;
        public IColumnService ColumnService { get; set; }

        public TicketService(IColumnService columnService)
        {
            var connectionString = ConfigurationHelper.Instance.GetConfig<string>("DatabaseSettings:ConnectionString");
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(ConfigurationHelper.Instance.GetConfig<string>("DatabaseSettings:DatabaseName"));

            _tickets = database.GetCollection<Ticket>($"{typeof(Ticket).Name}");

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