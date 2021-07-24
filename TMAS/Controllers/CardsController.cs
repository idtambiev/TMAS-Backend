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
using TMAS.DB.DTO;

namespace TMAS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : BaseController
    {
        private readonly CardService _cardsService;
        public CardsController(CardService service)
        {
            _cardsService = service;
        }

        [HttpGet("search")]
        [Authorize]
        public async Task<IActionResult> FindBoards(int boardId,string text)
        {
            return Ok(await _cardsService.FindCard(boardId, text));
        }

        [HttpGet("get")]
        [Authorize]
        public async Task<ActionResult<Card>> GetCards(int id)
        {
            return Ok(await _cardsService.GetAll(id));
        }
        [HttpGet("update/check")]
        [Authorize]
        public async Task<ActionResult<Card>> UpdatecardCheck(int id,string isDone)
        {
            return Ok(await _cardsService.CheckCard(id,Convert.ToBoolean(isDone)));
        }
        [HttpGet("get/one")]
        [Authorize]
        public async Task<ActionResult<Card>> GetOneCard(int cardId)
        {
            return Ok(await _cardsService.GetOne(cardId));
        }


        [HttpPost("create")]
        [Authorize]
        public async Task<ActionResult<Card>> CreateNewCard([FromBody]Card card)
        {
            return Ok(await _cardsService.Create(card));
        }


        [HttpPut("update")]
        [Authorize]
        public async Task<ActionResult<Card>> UpdateCard(Card card)
        {
            return Ok(await _cardsService.Update(card));
        }
        [HttpPut("move")]
        [Authorize]
        public async Task<ActionResult<Card>> MoveCard(Card card)
        {
            return Ok(await _cardsService.Move(card));
        }

        [HttpPut("moveoncolumn")]
        [Authorize]
        public async Task<ActionResult<Card>> MoveOnColumnCard(Card card)
        {
            return Ok(await _cardsService.MoveOnColumns(card));
        }
        [HttpDelete("delete")]
        [Authorize]
        public async Task<ActionResult<Card>> DeleteCard(int id)
        {
            return Ok(await _cardsService.Delete(id));
        }
    }
}
