using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DAL.Repositories;
using TMAS.BLL.Interfaces;
using TMAS.DB.Models;
using Microsoft.AspNetCore.Identity;
using TMAS.DB.DTO;
using AutoMapper;
using FluentValidation;
using TMAS.BLL.Validator;

namespace TMAS.BLL.Services
{
   public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly AbstractValidator<RegistrateUserDto> _userValidator;
        public UserService(UserRepository repository, UserManager<User> userManager,IMapper mapper,AbstractValidator<RegistrateUserDto> validator)
        {
            _userRepository = repository;
             _userManager = userManager;
            _mapper = mapper;
            _userValidator = validator;
        }
        public async Task<User> GetOneByEmail(User user)
        {
           var findedUser= await _userManager.FindByEmailAsync(user.Email);
           return findedUser;
        }

        public async Task<IdentityResult> Create(RegistrateUserDto createdUser)
        {
            var validationResult = _userValidator.Validate(createdUser);

            if (!validationResult.IsValid)
            {
                throw new Exception(validationResult.ToString());
            }
            else
            {
                var findedUser = await _userManager.FindByEmailAsync(createdUser.Email);
                if (findedUser == null)
                {
                    var newUser = _mapper.Map<RegistrateUserDto, User>(createdUser);
                    var result = await _userManager.CreateAsync(newUser, createdUser.Password);
                    return result;
                }
                else return default;
            }
        }
    }
}
