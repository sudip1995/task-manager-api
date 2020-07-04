using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using GraphQL;
using MongoDB.Driver;
using TaskManager.Contracts.Models;
using TaskManager.Library.Database;
using TaskManager.Library.DataProviders;
using TaskManager.Library.Extensions;
using TaskManager.Library.Helpers;
using TaskManager.Library.Models;

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
        [Import] 
        public IGenericRepository<TicketDetails> TicketDetailsRepository { get; set; }
        [Import] public IUserInfoProvider UserInfoProvider { get; set; }

        public Ticket Get(string id)
        {
            var filter = Builders<Board>.Filter.Eq("Columns.Tickets._id", id);
            var board = Repository.GetItemsByFilter(filter).FirstOrDefault();
            if (board == null)
            {
                throw new Exception("Board not found");
            }
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

            var user = UserInfoProvider.GetUser();
            var ticket = new Ticket(title, columnId, user);
            column.AddTicket(ticket);
            board.UpdateColumn(column);
            BoardService.Update(board.Id, board);

            AddTicketDetails(ticket);
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

        public TicketDetails GetDetails(string id)
        {
            return TicketDetailsRepository.Get(id);
        }

        public List<CheckList> GetCheckLists(string ticketId)
        {
            var ticketDetails = TicketDetailsRepository.Get(ticketId);
            return ticketDetails.CheckLists;
        }

        public CheckList AddCheckList(string title, string ticketId)
        {
            var ticket = Get(ticketId);
            var ticketDetails = GetDetails(ticketId);

            var checkList = new CheckList(title);

            ticketDetails.Add(checkList);
            ticket.CheckListCount++;

            TicketDetailsRepository.Update(ticketId, ticketDetails);

            Update(ticketId, ticket);

            return checkList;
        }

        public CheckListItem AddCheckListItem(string title, string checklistId)
        {
            var checkList = GetCheckList(checklistId);
            var checkListItem = new CheckListItem(title);
            checkList.CheckListItems.Add(checkListItem);

            var filter = Builders<TicketDetails>.Filter.Eq("CheckLists._id", checklistId);

            var update = Builders<TicketDetails>.Update;
            var checkListUpdate = update.Set("CheckLists.$.CheckListItems", checkList.CheckListItems);
            TicketDetailsRepository.UpdateOneByFilter(filter, checkListUpdate);

            return checkListItem;
        }

        public CheckList UpdateCheckList(string id, CheckList checklist)
        {
            var currentCheckList = GetCheckList(id);
            if (currentCheckList == null)
            {
                throw new Exception($"Can't find any checkList with id {id}");
            }
            var updatedCheckList = new CheckList();

            foreach (var propertyInfo in checklist.GetType().GetProperties())
            {
                updatedCheckList.GetType().GetProperty(propertyInfo.Name)?.SetValue(updatedCheckList, checklist.GetPropertyValue(propertyInfo.Name) ?? currentCheckList.GetPropertyValue(propertyInfo.Name));
            }

            var filter = Builders<TicketDetails>.Filter.Eq("CheckLists._id", id);
            var update = Builders<TicketDetails>.Update;
            var checkListUpdate = update.Set("CheckLists.$", updatedCheckList);

            TicketDetailsRepository.UpdateOneByFilter(filter, checkListUpdate);


            return updatedCheckList;
        }

        public CheckListItem UpdateCheckListItem(string id, CheckListItem checklistItem)
        {
            var currentCheckList = GetCheckListWithChecklistItemId(id);
            var currentCheckListItem = currentCheckList.CheckListItems.FirstOrDefault(o => o.Id == id);
            if (currentCheckListItem == null)
            {
                throw new Exception($"Can't find any checkListItem with id {id}");
            }
            var updatedCheckListItem = new CheckListItem();

            foreach (var propertyInfo in checklistItem.GetType().GetProperties())
            {
                updatedCheckListItem.GetType().GetProperty(propertyInfo.Name)?.SetValue(updatedCheckListItem, checklistItem.GetPropertyValue(propertyInfo.Name) ?? currentCheckListItem.GetPropertyValue(propertyInfo.Name));
            }

            var index = currentCheckList.CheckListItems.FindIndex(o => o.Id == id);
            if (index != -1)
            {
                currentCheckList.CheckListItems[index] = updatedCheckListItem;
            }

            var filter = Builders<TicketDetails>.Filter.Eq("CheckLists._id", currentCheckList.Id);
            var update = Builders<TicketDetails>.Update;
            var checkListUpdate = update.Set("CheckLists.$", currentCheckList);

            TicketDetailsRepository.UpdateOneByFilter(filter, checkListUpdate);


            return updatedCheckListItem;
        }


        #region PrivateMethod
        private void AddTicketDetails(Ticket ticket)
        {
            var ticketDetails = new TicketDetails {Id = ticket.Id, Title = ticket.Title};

            TicketDetailsRepository.InsertOne(ticketDetails);
        }

        private CheckList GetCheckList(string id)
        {
            var filter = Builders<TicketDetails>.Filter.Eq("CheckLists._id", id);
            var ticketDetails = TicketDetailsRepository.GetItemsByFilter(filter).FirstOrDefault();
            if (ticketDetails == null)
            {
                throw new Exception("Ticket not found");
            }
            var checkList = ticketDetails.CheckLists.FirstOrDefault(o => o.Id == id);
            return checkList;
        }

        private object GetCheckListItem(string id)
        {
            var filter = Builders<TicketDetails>.Filter.Eq("CheckLists.CheckListItems._id", id);
            var ticketDetails = TicketDetailsRepository.GetItemsByFilter(filter).FirstOrDefault();
            if (ticketDetails == null)
            {
                throw new Exception("Ticket not found");
            }

            var checkList = ticketDetails.CheckLists.FirstOrDefault(o => o.CheckListItems.Exists(e => e.Id == id));
            var checkListItem = checkList?.CheckListItems.FirstOrDefault(o => o.Id == id);
            return checkListItem;
        }

        private CheckList GetCheckListWithChecklistItemId(string id)
        {
            var filter = Builders<TicketDetails>.Filter.Eq("CheckLists.CheckListItems._id", id);
            var ticketDetails = TicketDetailsRepository.GetItemsByFilter(filter).FirstOrDefault();
            if (ticketDetails == null)
            {
                throw new Exception("Ticket not found");
            }

            var checkList = ticketDetails.CheckLists.FirstOrDefault(o => o.CheckListItems.Exists(e => e.Id == id));
            return checkList;
        }

        #endregion

    }
}