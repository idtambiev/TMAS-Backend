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
    public class CardsContentValidator:AbstractValidator<CardContentDTO>
    {
        public CardsContentValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .WithMessage("Incorrect Id");

            RuleFor(x => x.ExecutionPeriod)
                .NotEmpty()
                .NotNull()
                .WithMessage("Incorrect Execution Period");
        }
    }
}
