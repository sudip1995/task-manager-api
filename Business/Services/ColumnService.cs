using System;
using System.Collections.Generic;
using MongoDB.Driver;
using TaskManager.Contracts.Models;

namespace TaskManager.Business.Services
{
    public class ColumnService : IColumnService
    {
        private readonly IMongoCollection<Column> _columns;
        public IBoardService BoardService { get; set; }

        public ColumnService(ITaskManagerStoreDatabaseSettings settings,
            IBoardService boardService)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _columns = database.GetCollection<Column>(settings.ColumnCollectionName);
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