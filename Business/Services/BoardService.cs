using System;
using System.Composition;

namespace TaskManager.Business.Services
{
    [Export(typeof(IBoardService))]
    public class BoardService: IBoardService
    {
        public void AddBoard()
        {
            Console.WriteLine("Board Added");
        }
    }
}