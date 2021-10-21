using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DAL.DTO;
using TMAS.DAL.DTO.Created;

namespace TMAS.BLL.Validator
{
    public class CardsValidator:AbstractValidator<CardCreatedDTO>
    {
        public CardsValidator()
        {
            RuleFor(x => x.ColumnId)
                .NotNull()
                .WithMessage("Incorrect ColumnId");

            RuleFor(x => x.SortBy)
                .NotNull()
                .WithMessage("Incorrect SortBy");

            RuleFor(x => x.Title)
                .NotEmpty()
                .NotNull()
                .WithMessage("Incorrect Title");

        }
    }
}
