using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DB.DTO;

namespace TMAS.BLL.Validator
{
    public class UserValidator:AbstractValidator<RegistrateUserDto>
    {
        public UserValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(30).WithMessage("Incorrect Name");
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(30).WithMessage("Incorrect Name");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Empty Username");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("bad email");
            RuleFor(x => x.Password).NotEmpty()
                .MinimumLength(6).WithMessage("Incorrect Password"); ;
        }
    }
}
