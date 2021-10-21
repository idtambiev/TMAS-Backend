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
    public class AccessValidator:AbstractValidator<AccessCreatedDTO>
    {
        public AccessValidator()
        {
            RuleFor(x => x.BoardId).NotEmpty()
                .NotNull()
                .WithMessage("Incorrect BoardId");

            RuleFor(x => x.UserId).NotEmpty()
                .NotNull()
                .WithMessage("Incorrect UserId");

            
        }
    }
}
