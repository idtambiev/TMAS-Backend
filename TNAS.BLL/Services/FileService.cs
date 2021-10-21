using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.BLL.Interfaces;
using TMAS.DAL.DTO;
using TMAS.DAL.DTO.View;
using TMAS.DAL.Interfaces;
using TMAS.DAL.Repositories;
using TMAS.DB.Models;

namespace TMAS.BLL.Services
{
    public class FileService:IFileService
    {
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;
        private readonly AbstractValidator<File> _fileValidator;

        public FileService(IFileRepository fileRepository,IMapper mapper, AbstractValidator<File> fileValidator)
        {
            _fileRepository = fileRepository;
            _mapper = mapper;
            _fileValidator = fileValidator;
        }
        public async Task<File> Create(int cardId,string path,string type,string name)
        {
            var newFile = new File
            {
                Name=name,
                Path=path,
                CardId=cardId,
                FileType=type
            };
            var validationResult = _fileValidator.Validate(newFile);

            if (!validationResult.IsValid)
            {
                throw new Exception(validationResult.ToString());
            }
            else
            {
                var createResult = await _fileRepository.Create(newFile);
                return createResult;
            }
        }

        public async Task<IEnumerable<FileViewDTO>> GetFiles(int cardId)
        {
            if (cardId != null)
            {
                var result = await _fileRepository.GetFiles(cardId);
                var mapperResult = _mapper.Map<IEnumerable<File>, IEnumerable<FileViewDTO>>(result);
                return mapperResult;
            }
            else
            {
                throw new Exception("Empty card id");
            }
        }
    }
}
