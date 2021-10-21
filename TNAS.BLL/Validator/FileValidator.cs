using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DAL.DTO;
using TMAS.DAL.DTO.Created;
using TMAS.DAL.DTO.View;
using TMAS.DB.Models;

namespace TMAS.BLL.Validator
{
    public class FileValidator:AbstractValidator<File>
    {
        public FileValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Incorrect Name");

            RuleFor(x => x.CardId)
                .NotNull()
                .WithMessage("Incorrect card id");

            RuleFor(x => x.FileType)
                .NotEmpty()
                .NotNull()
                .WithMessage("Incorrect fyle type");

            RuleFor(x => x.Path)
                .NotEmpty()
                .NotNull()
                .WithMessage("Incorrect path");

        }
    }
}
