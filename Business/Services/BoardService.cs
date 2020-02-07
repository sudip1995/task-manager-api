using System.Collections.Generic;
using System.Composition;
using System.Threading.Tasks;
using MongoDB.Driver;
using TaskManager.Contracts.Models;
using TaskManager.Library;

namespace TaskManager.Business.Services
{
    [Export(typeof(IBoardService))]
    public class BoardService: IBoardService
    {
        private readonly IMongoCollection<Board> _boards;

        public BoardService()
        {
            var connectionString = ConfigurationHelper.Instance.GetConfig<string>("DatabaseSettings:ConnectionString");
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(ConfigurationHelper.Instance.GetConfig<string>("DatabaseSettings:DatabaseName"));

            _boards = database.GetCollection<Board>($"{typeof(Board).Name}");
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

        public List<Board> GetAll()
        {
            return _boards.Find(_ => true).ToList();
        }
    }
}