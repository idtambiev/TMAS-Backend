using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DAL.DTO;
using TMAS.DAL.DTO.Created;
using TMAS.DAL.DTO.View;

namespace TMAS.BLL.Validator
{
    public class ColumnViewValidator:AbstractValidator<ColumnViewDTO>
    {
        public ColumnViewValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .WithMessage("Incorrect ColumnId");

            RuleFor(x => x.BoardId)
                .NotNull()
                .WithMessage("Incorrect board id");

            RuleFor(x => x.Title)
                .NotEmpty()
                .NotNull()
                .WithMessage("Incorrect Title");

            RuleFor(x => x.SortBy)
                .NotNull()
                .WithMessage("Incorrect sortby");

        }
    }
}
