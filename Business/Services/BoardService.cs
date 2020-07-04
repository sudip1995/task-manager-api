using System;
using System.Collections.Generic;
using System.Composition;
using System.Threading.Tasks;
using GraphQL;
using MongoDB.Bson;
using MongoDB.Driver;
using TaskManager.Contracts.Models;
using TaskManager.Library;
using TaskManager.Library.Database;
using TaskManager.Library.DataProviders;
using TaskManager.Library.Extensions;
using TaskManager.Library.Helpers;
using TaskManager.Library.Models;

namespace TaskManager.Business.Services
{
    [Export(typeof(IBoardService))]
    public class BoardService: IBoardService
    {
        [Import] 
        public IGenericRepository<Board> Repository { get; set; }
        [Import] 
        public IUserInfoProvider UserInfoProvider { get; set; }

        public Board Add(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new Exception("Cannot add a title without title");
            }

            var user = UserInfoProvider.GetUser();
            var board = new Board(title, user);

            Repository.InsertOne(board);
            return board;
        }

        public Board Get(string id)
        {
            return Repository.Get(id);
        }

        public List<Board> GetAll()
        {
            var user = UserInfoProvider.GetUser();
            var filter = Builders<Board>.Filter.Eq("CreatedBy", user);
            return Repository.GetItemsByFilter(filter);
        }

        public Board Update(string id, Board board)
        {
            var currentBoard = Get(id);
            if (currentBoard == null)
            {
                throw new Exception($"Can't find any title with id {id}");
            }

            var updatedBoard = new Board();

            foreach (var propertyInfo in board.GetType().GetProperties())
            {
                updatedBoard.GetType().GetProperty(propertyInfo.Name)?.SetValue(updatedBoard, board.GetPropertyValue(propertyInfo.Name) ?? currentBoard.GetPropertyValue(propertyInfo.Name));
            }
            Repository.Update(id, updatedBoard);

            return updatedBoard;
        }

        public Board MoveColumn(string fromBoardId, string toBoardId, int previousIndex, int currentIndex)
        {
            var fromBoard = Get(fromBoardId);
            var column = fromBoard.Columns[previousIndex];
            fromBoard.Columns.RemoveAt(previousIndex);
            var toBoard = fromBoard;
            if (!string.IsNullOrEmpty(toBoardId))
            {
                toBoard = Get(toBoardId);
            }

            column.BoardId = toBoard.Id;
            toBoard.Columns.Insert(currentIndex, column);
            Update(fromBoard.Id, fromBoard);
            if (fromBoard.Id != toBoard.Id)
            {
                Update(toBoard.Id, toBoard);
            }

            return fromBoard;
        }
    }
}