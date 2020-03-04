using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
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
        public IBoardService BoardService { get; set; }
        [Import]
        public IColumnService ColumnService { get; set; }
        [Import]
        public IGenericRepository<Board> Repository { get; set; }

        public Ticket Get(string id)
        {
            var filter = Builders<Board>.Filter.Eq("Columns.Tickets._id", id);
            var board = Repository.GetItemsByFilter(filter)[0];
            var column = board.Columns.FirstOrDefault(o => o.Tickets.Exists(t => t.Id == id));
            return column?.Tickets.FirstOrDefault(o => o.Id == id);
        }

        public List<Ticket> GetAll(string columnId)
        {
            var column = ColumnService.Get(columnId);
            return column.Tickets;
        }

        public Ticket Add(string title, string columnId)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new Exception("Cannot add a title without title");
            }

            var column = ColumnService.Get(columnId);
            if (column == null)
            {
                throw new Exception($"Column with id {columnId} doesn't exist");
            }
            var board = BoardService.Get(column.BoardId);
            if (board == null)
            {
                throw new Exception($"Board with id {column.BoardId} doesn't exist");
            }

            var ticket = new Ticket(title, columnId);
            column.AddTicket(ticket);
            board.UpdateColumn(column);
            BoardService.Update(board.Id, board);
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

            var column = ColumnService.Get(ticket.ColumnId);
            column.UpdateTicket(ticket);
            var board = BoardService.Get(column.BoardId);
            board.UpdateColumn(column);
            BoardService.Update(board.Id, board);
            return updatedTicket;
        }
    }
}