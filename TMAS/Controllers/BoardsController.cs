using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMAS.BLL.Services;
using TMAS.DB.Models;
using TMAS.DAL.DTO;
using Microsoft.AspNetCore.Authorization;
using TMAS.Controllers.Base;
using TMAS.BLL.Interfaces;
using TMAS.DAL.DTO.View;

namespace TMAS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardsController : BaseController
    {
       private readonly IBoardService _boardService;
        public BoardsController(IBoardService service)
        {
            _boardService = service;
        }

        [HttpGet("get")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<BoardViewDTO>>> GetBoards()
        {
            var userId = GetUserId();
            var boards = await _boardService.GetAll(userId);
            return Ok(boards);
        }


        [HttpGet("get/one")]
        [Authorize]
        public async Task<ActionResult<BoardViewDTO>> GetOneBoard([FromQuery] int id)
        {
            var board = await _boardService.GetOne(id);
            return Ok(board);
        }

        [HttpGet("search")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<BoardViewDTO>>> FindBoards([FromQuery]string text)
        {
            var userId = GetUserId();
            var boards = await _boardService.FindBoard(userId, text);
            return Ok(boards);
        }

        [HttpGet("create")]
        [Authorize]
        public async Task<ActionResult<BoardViewDTO>> CreateNewBoard([FromQuery] string title)
        {
            var id = GetUserId();
            var createResult = await _boardService.Create(title, id);
            return Ok(createResult);
        }
    }
}
