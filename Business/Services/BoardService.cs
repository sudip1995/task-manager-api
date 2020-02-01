using System.Collections.Generic;
using System.Composition;
using System.Threading.Tasks;
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

        public Board Add(Board board)
        {
            _boards.InsertOne(board);
            return board;
        }

        public Board Get(string id)
        {
            return _boards.Find(board => board.Id == id).FirstOrDefault();
        }

        public Task<List<Board>> GetAll()
        {
            return _boards.Find(_ => true).ToListAsync();
        }
    }
}