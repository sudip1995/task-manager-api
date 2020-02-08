using System;
using System.Collections.Generic;
using System.Composition;
using GraphQL;
using MongoDB.Driver;
using TaskManager.Contracts.Models;
using TaskManager.Library;
using TaskManager.Library.Extensions;
using TaskManager.Library.Helpers;
using TaskManager.Library.Ioc;

namespace TaskManager.Business.Services
{
    [Export(typeof(IColumnService))]
    public class ColumnService : IColumnService
    {
        private readonly IMongoCollection<Column> _columns;
        [Import]
        public IBoardService BoardService { get; set; }

        public ColumnService()
        {
            var connectionString = ConfigurationHelper.Instance.GetDatabaseConnectionString();
            var client = new MongoClient(connectionString);
            var databaseName = ConfigurationHelper.Instance.GetDatabaseName();
            var database = client.GetDatabase(databaseName);

            _columns = database.GetCollection<Column>($"{typeof(Column).Name}");
        }
        public List<Column> GetAll(string boardId)
        {
            return _columns.Find(column => column.BoardId == boardId).ToList();
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
            column.Order = columnCount;
            _columns.InsertOne(column);
            return column;
        }

        public Column Get(string id)
        {
            return _columns.Find(column => column.Id == id).FirstOrDefault();
        }

        public Column Update(string id, Column column)
        {
            var currentColumn = Get(id);
            if (currentColumn == null)
            {
                throw new Exception($"Can't find any column with id {id}");
            }
            var filter = Builders<Column>.Filter.Eq(o => o.Id, id);
            var updatedColumn = new Column();

            foreach (var propertyInfo in column.GetType().GetProperties())
            {
                updatedColumn.GetType().GetProperty(propertyInfo.Name)?.SetValue(updatedColumn, column.GetPropertyValue(propertyInfo.Name) ?? currentColumn.GetPropertyValue(propertyInfo.Name));
            }

            _columns.ReplaceOne(filter, updatedColumn);
            return updatedColumn;
        }
    }
}