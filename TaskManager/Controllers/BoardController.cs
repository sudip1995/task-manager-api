using System;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Business.Services;
using TaskManager.Contracts.Models;


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

        [HttpPost("AddBoard")]
        public IActionResult Add([FromBody] Board board)
        {
            try
            {
                return Ok(BoardService.Add(board));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            
        }

        [HttpGet("Get")]
        public IActionResult Get([FromQuery] string title)
        {
            try
            {
                var board = BoardService.Get(title);
                return Ok(board);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            
        }
    }
}