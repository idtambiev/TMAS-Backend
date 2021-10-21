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
    public class CardsViewValidator:AbstractValidator<CardViewDTO>
    {
        public CardsViewValidator()
        {
            RuleFor(x => x.ColumnId)
                .NotNull()
                .WithMessage("Incorrect ColumnId");

            RuleFor(x => x.SortBy)
                .NotNull()
                .WithMessage("Incorrect SortBy");

            RuleFor(x => x.Title)
                .NotNull()
                .NotEmpty()
                .WithMessage("Incorrect Title");


            RuleFor(x => x.Id)
               .NotNull()
               .WithMessage("Incorrect card id");
        }
    }
}
