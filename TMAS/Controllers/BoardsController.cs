using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMAS.BLL.Services;
using TMAS.DB.Models;
using TMAS.DB.DTO;
using Microsoft.AspNetCore.Authorization;
using TMAS.Controllers.Base;

namespace TMAS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardsController : BaseController
    {
       private readonly BoardService _boardService;
        public BoardsController(BoardService service)
        {
            _boardService = service;
        }

        [HttpGet("get")]
        [Authorize]
        public async Task<IActionResult> GetBoards()
        {
            var userId = GetUserId();
            return Ok(await _boardService.GetAll(userId));
        }

        [HttpGet("get/one")]
        [Authorize]
        public async Task<IActionResult> GetOneBoard(int boardId)
        {
            return Ok(await _boardService.GetOne(boardId));
        }

        [HttpGet("search")]
        [Authorize]
        public async Task<IActionResult> FindBoards(string text)
        {
            var userId = GetUserId();
            return Ok(await _boardService.FindBoard(userId, text));
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<ActionResult<Board>> CreateNewBoard(string title)
        {
            var id = GetUserId();
            return Ok(await _boardService.Create(title, id));
        }

        [HttpPut("update")]
        [Authorize]
        public async Task<ActionResult<Board>> UpdateBoard(Board board)
        {
            return Ok(await _boardService.Update(board));
        }

        [HttpDelete("delete")]
        [Authorize]
        public async Task<ActionResult<Board>> DeleteBoard(int boardId)
        {
            return Ok(await _boardService.Delete(boardId));
        }
    }
}
