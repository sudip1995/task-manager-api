using System;
using System.Composition;
using MongoDB.Driver;
using TaskManager.Contracts.Models;

namespace TaskManager.Business.Services
{
    [Export(typeof(IBoardService))]
    public class BoardService: IBoardService
    {
        private readonly IMongoCollection<Board> _boards;

        public BoardService(IBoardStoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _boards = database.GetCollection<Board>(settings.BoardCollectionName);
        }

        public void Add(Board board)
        {
            _boards.InsertOne(board);
            Console.WriteLine("Board Added");
        }

        public Board Get(string title)
        {
            return _boards.Find(board => board.Title == title).FirstOrDefault();
        }
    }
}