using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMAS.BLL.Services;
using TMAS.DB.Models;
using TMAS.Controllers.Base;
using TMAS.DAL.DTO;
using TMAS.BLL.Interfaces;
using TMAS.DAL.DTO.View;

namespace TMAS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColumnsController : BaseController
    {
        private readonly IColumnService _columnService;

        public ColumnsController(IColumnService service)
        {
            _columnService = service;
        }

        [HttpGet("get")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ColumnViewDTO>>> GetColumns([FromQuery]int id)
        {
            var columns = await _columnService.GetAll(id);
            return Ok(columns);
        }

        [HttpGet("get/one")]
        [Authorize]
        public async Task<ActionResult<ColumnViewDTO>> GetOneColumn([FromQuery] int id)
        {
            var column = await _columnService.GetOne(id);
            return Ok(column);
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<ActionResult<ColumnViewDTO>> CreateNewColumn([FromBody]ColumnViewDTO column)
        {
            Guid userId = GetUserId();
            var createResult = await _columnService.Create(column,userId);
            return Ok(createResult);
        }

        [HttpGet("update")]
        [Authorize]
        public async Task<ActionResult<ColumnViewDTO>> UpdateColumn([FromQuery] int id,string title)
        {
            Guid userId = GetUserId();
            var updateResult = await _columnService.UpdateTitle(id,title,userId);
            return Ok(updateResult);
        }

        [HttpDelete("delete")]
        [Authorize]
        public async Task<ActionResult<ColumnViewDTO>> DeleteColumn([FromQuery] int id)
        {
            Guid userId = GetUserId();
            var column = await _columnService.Delete(id,userId);
            return Ok(column);
        }

        [HttpPut("move")]
        [Authorize]
        public async Task<ActionResult<ColumnViewDTO>> MoveColumn([FromBody] ColumnViewDTO column)
        {
            Guid userId = GetUserId();
            var moveResult = await _columnService.Move(column,userId);
            return Ok(moveResult);
        }

    }
}
