using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using GraphQL;
using MongoDB.Bson;
using MongoDB.Driver;
using TaskManager.Contracts.Models;
using TaskManager.Library;
using TaskManager.Library.Database;
using TaskManager.Library.Extensions;
using TaskManager.Library.Helpers;
using TaskManager.Library.Ioc;

namespace TaskManager.Business.Services
{
    [Export(typeof(IColumnService))]
    public class ColumnService : IColumnService
    {
        [Import]
        public IBoardService BoardService { get; set; }
        [Import]
        public IGenericRepository<Board> Repository { get; set; }
        public List<Column> GetAll(string boardId)
        {
            var board = BoardService.Get(boardId);
            return board.Columns;
        }

        public Column Add(string title, string boardId)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new Exception("Cannot add a column without title");
            }
            var board = BoardService.Get(boardId);
            if (board == null)
            {
                throw new Exception($"Board with id {boardId} doesn't exist");
            }

            var column = new Column(title, boardId);
            board.AddColumn(column);
            BoardService.Update(boardId, board);
            return column;
        }

        public Column Get(string id)
        {
            var filter = Builders<Board>.Filter.Eq("Columns._id", id);
            var board = Repository.GetItemsByFilter(filter)[0];
            return board.Columns.FirstOrDefault(o => o.Id == id);
        }

        public Column Update(string id, Column column)
        {
            var currentColumn = Get(id);
            if (currentColumn == null)
            {
                throw new Exception($"Can't find any column with id {id}");
            }
            var updatedColumn = new Column();

            foreach (var propertyInfo in column.GetType().GetProperties())
            {
                updatedColumn.GetType().GetProperty(propertyInfo.Name)?.SetValue(updatedColumn, column.GetPropertyValue(propertyInfo.Name) ?? currentColumn.GetPropertyValue(propertyInfo.Name));
            }

            var board = BoardService.Get(column.BoardId);
            board.UpdateColumn(updatedColumn);
            BoardService.Update(column.BoardId, board);

            return updatedColumn;
        }

        public Board MoveTicket(string fromBoardId, string toBoardId, string fromColumnId, string toColumnId, int previousIndex, int currentIndex)
        {
            var fromBoard = BoardService.Get(fromBoardId);
            var fromColumn = fromBoard.Columns.FirstOrDefault(o => o.Id == fromColumnId);

            var ticket = fromColumn.Tickets[previousIndex];
            fromColumn.Tickets.RemoveAt(previousIndex);
            fromBoard.UpdateColumn(fromColumn);
            
            var toBoard = fromBoard;
            var toColumn = fromColumn;

            if (!string.IsNullOrEmpty(toBoardId))
            {
                if (string.IsNullOrEmpty(toColumnId))
                {
                    throw new Exception("Can't move ticket to another board if toColumn not specified");
                }

                toBoard = BoardService.Get(toBoardId);
                toColumn = toBoard.Columns.FirstOrDefault(o => o.Id == toColumnId);
            }
            else
            {
                if (!string.IsNullOrEmpty(toColumnId))
                {
                    toColumn = toBoard.Columns.FirstOrDefault(o => o.Id == toColumnId);
                }
            }

            ticket.ColumnId = toColumn.Id;

            toColumn.Tickets.Insert(currentIndex, ticket);
            toBoard.UpdateColumn(toColumn);
            BoardService.Update(fromBoard.Id, fromBoard);
            if (fromBoard.Id != toBoard.Id)
            {
                BoardService.Update(toBoard.Id, toBoard);
            }


            return toBoard;
        }
    }
}