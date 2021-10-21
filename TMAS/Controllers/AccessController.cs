using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMAS.BLL.Interfaces;
using TMAS.BLL.Services;
using TMAS.Controllers.Base;
using TMAS.DAL.DTO;
using TMAS.DAL.DTO.Created;
using TMAS.DAL.DTO.View;
using TMAS.DB.Models;

namespace TMAS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessController : BaseController
    {
        private readonly IBoardAccessService _boardsAccesService;
        public AccessController(IBoardAccessService boardsAccesService)
        {
            _boardsAccesService = boardsAccesService;
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<ActionResult<AccessCreatedDTO>> CreateBoardsAccess([FromBody]AccessCreatedDTO access) 
        {
            var data = await _boardsAccesService.Create(access);

            return Ok(data);
        }

        [HttpGet("get")]
        [Authorize]
        public async Task<ActionResult<BoardViewDTO>> Get()
        {
            var userId = GetUserId();
            var board = await _boardsAccesService.Get(userId);
            return Ok(board);
        }

        [HttpGet("get/all/users")]
        [Authorize]
        public async Task<ActionResult<UserDTO>> GetUsers([FromQuery]int id,string text)
        {
            var userId = GetUserId();
            return Ok(await _boardsAccesService.GetAllUsers(id, text, userId));
        }


        [HttpGet("get/assigned/users")]
        [Authorize]
        public async Task<ActionResult<UserDTO>> GetAssignedUsers([FromQuery]int id,string text)
        {
            var userId = GetUserId();
            return Ok(await _boardsAccesService.GetAssignedUsers(id,text,userId));
        }

        [HttpDelete("delete")]
        [Authorize]
        public async Task<ActionResult> GetUsers([FromQuery]int boardId,Guid userId)
        {
            _ = await _boardsAccesService.Delete(boardId, userId);
            return Ok();
        }
    }
}
