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
using TaskManager.Library.Extensions;
using TaskManager.Library.Helpers;

namespace TaskManager.Business.Services
{
    [Export(typeof(IBoardService))]
    public class BoardService: IBoardService
    {
        [Import] 
        public IGenericRepository<Board> Repository { get; set; }

        public Board Add(Board board)
        {
            if (string.IsNullOrWhiteSpace(board.Title))
            {
                throw new Exception("Cannot add a board without title");
            }

            Repository.InsertOne(board);
            return board;
        }

        public Board Get(string id)
        {
            return Repository.GetById(id);
        }

        public List<Board> GetAll()
        {
            return Repository.GetAll();
        }

        public Board Update(string id, Board board)
        {
            var currentBoard = Get(id);
            if (currentBoard == null)
            {
                throw new Exception($"Can't find any board with id {id}");
            }

            var updatedBoard = new Board();

            foreach (var propertyInfo in board.GetType().GetProperties())
            {
                updatedBoard.GetType().GetProperty(propertyInfo.Name)?.SetValue(updatedBoard, board.GetPropertyValue(propertyInfo.Name) ?? currentBoard.GetPropertyValue(propertyInfo.Name));
            }
            Repository.Update(id, updatedBoard);

            return updatedBoard;
        }
    }
}