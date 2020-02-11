using System;
using System.Collections.Generic;
using System.Composition;
using GraphQL;
using MongoDB.Driver;
using TaskManager.Contracts.Models;
using TaskManager.Library.Database;
using TaskManager.Library.Extensions;
using TaskManager.Library.Helpers;

namespace TaskManager.Business.Services
{
    [Export(typeof(ITicketService))]
    public class TicketService : ITicketService
    {
        
        [Import]
        public IColumnService ColumnService { get; set; }
        [Import] public IGenericRepository<Ticket> Repository { get; set; } 
        public Ticket Get(string id)
        {
            return Repository.Get(id);
        }

        public List<Ticket> GetAll(string columnId)
        {
            return Repository.GetItemsByCondition(e => e.ColumnId == columnId);
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
            Repository.InsertOne(ticket);
            return ticket;
        }

        public Ticket Update(string id, Ticket ticket)
        {
            var currentTicket = Get(id);
            if (currentTicket == null)
            {
                throw new Exception($"Can't find any ticket with id {id}");
            }
            var updatedTicket = new Ticket();

            foreach (var propertyInfo in ticket.GetType().GetProperties())
            {
                updatedTicket.GetType().GetProperty(propertyInfo.Name)?.SetValue(updatedTicket, ticket.GetPropertyValue(propertyInfo.Name) ?? currentTicket.GetPropertyValue(propertyInfo.Name));
            }

            Repository.Update(id, updatedTicket);
            return updatedTicket;
        }
    }
}