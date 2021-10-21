using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TMAS.BLL.Interfaces;
using TMAS.BLL.Services;
using TMAS.DAL.DTO;
using TMAS.DAL.DTO.View;
using TMAS.DB.Models;

namespace TMAS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFileService _fileService;
        public FilesController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("upload")]
        [Authorize]
        public async Task<ActionResult> Upload([FromQuery]int id)
        {
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
                    var saveResult =await _fileService.Create(id,finalName,file.ContentType,fileName);
                    return Ok(new { dbPath });
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


        [HttpGet("get")]
        [Authorize]
        public async Task<ActionResult<FileViewDTO>> GetFiles([FromQuery]int id)
        {
            var result = await _fileService.GetFiles(id);
            return Ok(result);
        }

    }
}
