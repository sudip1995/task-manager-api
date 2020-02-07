using System;
using System.Collections.Generic;
using MongoDB.Driver;
using TaskManager.Contracts.Models;
using TaskManager.Library;

namespace TaskManager.Business.Services
{
    public class ColumnService : IColumnService
    {
        private readonly IMongoCollection<Column> _columns;
        public IBoardService BoardService { get; set; }

        public ColumnService(IBoardService boardService)
        {
            var connectionString = ConfigurationHelper.Instance.GetConfig<string>("DatabaseSettings:ConnectionString");
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(ConfigurationHelper.Instance.GetConfig<string>("DatabaseSettings:DatabaseName"));

            _columns = database.GetCollection<Column>($"{typeof(Column).Name}");
            BoardService = boardService;
        }
        public List<Column> GetAll(string boardId)
        {
            return _columns.Find(column => column.BoardId == boardId).ToList();
        }

        public Column Add(Column column)
        {
            var board = BoardService.Get(column.BoardId);
            if (board == null)
            {
                throw new Exception($"Board with id {column.BoardId} doesn't exist");
            }

            var columnCount = GetAll(column.BoardId).Count;
            column.Order = columnCount;
            _columns.InsertOne(column);
            return column;
        }

        public Column Get(string columnId)
        {
            return _columns.Find(column => column.Id == columnId).FirstOrDefault();
        }
    }
}