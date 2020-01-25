using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Business.Services;


namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    public class BoardController : ControllerBase
    {
        public IBoardService BoardService { get; set; }

        public BoardController(IBoardService boardService)
        {
            BoardService = boardService;
        }

        [HttpPost("Add")]
        public IActionResult Add()
        {
            try
            {
                BoardService.AddBoard();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
        }
    }
}