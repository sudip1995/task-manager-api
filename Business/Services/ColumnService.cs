using System;
using System.Collections.Generic;
using System.Composition;
using GraphQL;
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
        [Import] public IGenericRepository<Column> Repository { get; set; }
        public List<Column> GetAll(string boardId)
        {
            //return Repository.GetItemsByCondition();
            return Repository.GetItemsByCondition(e => e.BoardId == boardId);
        }

        public Column Add(Column column, string boardId)
        {
            if (string.IsNullOrWhiteSpace(column.Title))
            {
                throw new Exception("Cannot add a column without title");
            }
            var board = BoardService.Get(boardId);
            if (board == null)
            {
                throw new Exception($"Board with id {boardId} doesn't exist");
            }

            var columnCount = GetAll(boardId).Count;
            column.BoardId = boardId;
            column.Order = columnCount;
            Repository.InsertOne(column);
            return column;
        }

        public Column Get(string id)
        {
            return Repository.GetById(id);
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

            Repository.Update(id, updatedColumn);
            return updatedColumn;
        }
    }
}