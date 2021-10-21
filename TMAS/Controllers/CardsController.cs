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
using TMAS.DAL.DTO.Created;

namespace TMAS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : BaseController
    {
        private readonly ICardService _cardsService;
        public CardsController(ICardService service)
        {
            _cardsService = service;
        }

        [HttpGet("search")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CardViewDTO>>> FindBoards([FromQuery] int columnId,string text)
        {
            var cards = await _cardsService.FindCard(columnId, text);
            return Ok(cards);
        }

        [HttpGet("get")]
        [Authorize]
        public async Task<ActionResult<CardViewDTO>> GetCards([FromQuery] int id)
        {
            var cards = await _cardsService.GetAll(id);
            return Ok(cards);
        }

        [HttpGet("update/check")]
        [Authorize]
        public async Task<ActionResult<CardViewDTO>> UpdatecardCheck([FromQuery] int id,string isDone)
        {
            Guid userId = GetUserId();
            var result = await _cardsService.CheckCard(id, Convert.ToBoolean(isDone),userId);
            return Ok(result);
        }

        [HttpGet("get/one")]
        [Authorize]
        public async Task<ActionResult<CardFullDTO>> GetOneCard([FromQuery] int id)
        {
            var card = await _cardsService.GetOne(id);
            return Ok(card);
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<ActionResult<CardViewDTO>> CreateNewCard([FromBody]CardCreatedDTO card)
        {
            Guid userId = GetUserId();
            var createResult = await _cardsService.Create(card,userId);
            return Ok(createResult);
        }


        [HttpGet("update")]
        [Authorize]
        public async Task<ActionResult<CardViewDTO>> UpdateCardTitle([FromQuery]int id,string title)
        {
            Guid userId = GetUserId();
            var updateResult = await _cardsService.UpdateTitle(id,title,userId);
            return Ok();
        }

        [HttpPut("update/content")]
        [Authorize]
        public async Task<ActionResult<CardViewDTO>> UpdateContent([FromBody]CardContentDTO card)
        {
            Guid userId = GetUserId();
            var updateResult = await _cardsService.UpdateContent(card,userId);
            return Ok(updateResult);
        }

        [HttpPut("move")]
        [Authorize]
        public async Task<ActionResult<CardViewDTO>> MoveCard([FromBody]CardViewDTO card)
        {
            Guid userId = GetUserId();
            var result = await _cardsService.Move(card,userId);
            return Ok(result);
        }

        [HttpPut("moveoncolumn")]
        [Authorize]
        public async Task<ActionResult<CardViewDTO>> MoveOnColumnCard([FromBody]CardViewDTO card)
        {
            Guid userId = GetUserId();
            var result = await _cardsService.MoveOnColumns(card,userId);
            return Ok(result);
        }


        [HttpDelete("delete")]
        [Authorize]
        public async Task<ActionResult<CardViewDTO>> DeleteCard([FromQuery] int id)
        {
            Guid userId = GetUserId();
            var result = await _cardsService.Delete(id,userId);
            return Ok(result);
        }
    }
}
