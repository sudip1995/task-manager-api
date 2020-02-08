using System;
using System.Collections.Generic;
using System.Composition;
using System.Threading.Tasks;
using GraphQL;
using MongoDB.Bson;
using MongoDB.Driver;
using TaskManager.Contracts.Models;
using TaskManager.Library;
using TaskManager.Library.Extensions;
using TaskManager.Library.Helpers;

namespace TaskManager.Business.Services
{
    [Export(typeof(IBoardService))]
    public class BoardService: IBoardService
    {
        private readonly IMongoCollection<Board> _boards;

        public BoardService()
        {
            var connectionString = ConfigurationHelper.Instance.GetDatabaseConnectionString();
            var client = new MongoClient(connectionString);
            var databaseName = ConfigurationHelper.Instance.GetDatabaseName();
            var database = client.GetDatabase(databaseName);

            _boards = database.GetCollection<Board>($"{typeof(Board).Name}");
        }

        public Board Add(Board board)
        {
            if (string.IsNullOrWhiteSpace(board.Title))
            {
                throw new Exception("Cannot add a board without title");
            }
            _boards.InsertOne(board);
            return board;
        }

        public Board Get(string id)
        {
            return _boards.Find(board => board.Id == id).FirstOrDefault();
        }

        public List<Board> GetAll()
        {
            return _boards.Find(_ => true).ToList();
        }

        public Board Update(string id, Board board)
        {
            var currentBoard = Get(id);
            if (currentBoard == null)
            {
                throw new Exception($"Can't find any board with id {id}");
            }
            var filter = Builders<Board>.Filter.Eq(o => o.Id, id);
            var updatedBoard = new Board();

            foreach (var propertyInfo in board.GetType().GetProperties())
            {
                updatedBoard.GetType().GetProperty(propertyInfo.Name)?.SetValue(updatedBoard, board.GetPropertyValue(propertyInfo.Name) ?? currentBoard.GetPropertyValue(propertyInfo.Name));
            }

            _boards.ReplaceOne(filter, updatedBoard);
            return updatedBoard;
        }
    }
}