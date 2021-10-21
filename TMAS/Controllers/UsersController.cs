using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using TMAS.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using TMAS.Controllers.Base;
using System.Security.Claims;
using System.IO;
using System.Net.Http.Headers;
using TMAS.BLL.Interfaces;
using TMAS.DB.Models;
using TMAS.DAL.DTO;
using TMAS.BLL;
using TMAS.DAL.DTO.View;
using TMAS.DAL.DTO.Created;

namespace TMAS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;
        public UsersController(IUserService service )
        {
            _userService = service;
        }

        [HttpPost("create")]
        public async Task<ActionResult<UserCreatedDto>> Registrate([FromBody]UserCreatedDto model)
        {
            var createResult = await _userService.Create(model);
            return Ok(createResult);
        }

        [HttpGet("find")]
        public async Task<ActionResult<Response>> Search([FromQuery]string email)
        {
            var result = await _userService.Find(email);
            return Ok(result);
        }


        [HttpGet("get")]
        [Authorize]
        public async Task<ActionResult<UserDTO>> GetUserName()
        {
            var id = GetUserId();
            var user = await _userService.GetOneById(id);
            return Ok(user);
        }

        [HttpGet("getfull")]
        [Authorize]
        public async Task<ActionResult<UserDTO>> GetFullUSer(string id)
        {
            Guid idUser = Guid.Parse(id);
            var user = await _userService.GetOneById(idUser);
            return Ok(user);
        }

        [HttpGet("confirm/email")]
        public async Task<ActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
            {
                return NotFound();
            }

            var result = await _userService.ConfirmEmailAsync(userId, token);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest();
        }


        [HttpGet("reset")]
        public async Task<ActionResult> ResetPassword(string email) {
        
            var result = await _userService.ResetEmail(email);
            return Ok(result);
        }

        [HttpGet("confirm/new/password")]
        public async Task<ActionResult> ConfirmResetPassword(string userId, string token, string password)
        {
            var result = await _userService.ResetUserPassword(userId, token, password);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("upload/photo")]
        [Authorize]
        public async Task<ActionResult> Upload()
        {
            var id = GetUserId();
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Files");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    Guid guidName = Guid.NewGuid();
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var splitedName = fileName.Split('.');
                    var finalName = guidName.ToString() + '.' + splitedName[splitedName.Length - 1];
                    var fullPath = Path.Combine(pathToSave, finalName);
                    var dbPath = Path.Combine(folderName, finalName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    _ = await _userService.AddPhoto(id,finalName);
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
