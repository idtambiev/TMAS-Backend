using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DB.Models;

namespace TMAS.BLL.Validator
{
    public class HistoryValidator: AbstractValidator<History>
    {
        public HistoryValidator()
        {
            RuleFor(x => x.ActionObject)
                .NotEmpty()
                .NotNull()
                .WithMessage("Incorrect action object");

            RuleFor(x => x.ActionType)
                .NotNull()
                .WithMessage("Incorrect action type");

            RuleFor(x => x.AuthorId)
                .NotNull()
                .WithMessage("Incorrect author id");

            RuleFor(x => x.BoardId)
                .NotNull()
                .WithMessage("Incorrect board id");

        }
    }
}
