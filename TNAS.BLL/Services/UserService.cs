using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DAL.Repositories;
using TMAS.BLL.Interfaces;
using TMAS.DB.Models;
using Microsoft.AspNetCore.Identity;
using TMAS.DAL.DTO;
using AutoMapper;
using FluentValidation;
using TMAS.BLL.Validator;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using TMAS.BLL.Models;
using Microsoft.Extensions.Configuration;
using TMAS.DAL.DTO.View;
using TMAS.DAL.DTO.Created;

namespace TMAS.BLL.Services
{
   public class UserService:IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly AbstractValidator<UserCreatedDto> _userValidator;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;

        public UserService(UserManager<User> userManager,IMapper mapper,AbstractValidator<UserCreatedDto> validator,IEmailService emailService,IConfiguration configuration,ITokenService tokenService)

        {
            _userManager = userManager;
            _mapper = mapper;
            _userValidator = validator;
            _emailService = emailService;
            _configuration = configuration;
            _tokenService = tokenService;
        }
        public async Task<User> GetOneByEmail(User user)
        {
           var findedUser= await _userManager.FindByEmailAsync(user.Email);
           return findedUser;
        }

        public async Task<IEnumerable<UserDTO>> GetUsers(string searchText,Guid currentUserId,Guid creatorUserId)
        {
            if (searchText!=null) {
                var findedUsers = await _userManager.Users
                    .Where(x => x.UserName.Contains(searchText))
                    .Where(x => x.Id != currentUserId)
                    .Where(x => x.Id != creatorUserId)
                    .ToListAsync();
                var mapperResult = _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(findedUsers);
                return mapperResult;
            }
            else
            {
                throw new Exception("Empty search text");
            }
        }

        public async Task<UserCreatedDto> Create(UserCreatedDto createdUser)
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
                    var newUser = _mapper.Map<UserCreatedDto, User>(createdUser);
                    var result = await _userManager.CreateAsync(newUser, createdUser.Password);

                    var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                    string validToken = await _tokenService.CreateValidToken(confirmEmailToken);

                    string url = $"{_configuration["AppUrl"]}/confirmemail?userid={newUser.Id}&token={validToken}";
                    string content = $"<h1>Welcome to TMAS </h1><p>Please confirm your email by  <a href='{url}'>Clicking here</a></p>";

                    EmailOptions email = new EmailOptions {
                        Email = newUser.Email,
                        Content =content,
                        Subject = "Confirm your email",
                    };

                    await _emailService.SendEmailAsync(email);

                    return createdUser;
                }
                else return null;
            }
        }

        public async Task<Response> Find(string email)
        {
            if (email!=null)
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    return new Response
                    {
                        IsSuccess = true,
                        Message = "This email not used"
                    };
                }
                else
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "This email already in use"
                    };
                }
            }
            else
            {
                throw new Exception("Empty email");
            }
            
        }

        public async Task<UserDTO> GetOneById(Guid id)
        {
            User findedUser = await _userManager.FindByIdAsync(id.ToString());
            var result = _mapper.Map<User,UserDTO>(findedUser);
            result.Photo = _configuration["FileUrl"] + result.Photo;
            return result;
        }


        public async Task<Response> ConfirmEmailAsync(string id,string token)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)  return new Response
            {
                IsSuccess = false,
                Message = "User not found"
            }; ;

            var normalToken = await _tokenService.DecodingToken(token);
            var result = await _userManager.ConfirmEmailAsync(user,normalToken);

            if (result.Succeeded)
            {
                return new Response { 
                    IsSuccess=true,
                    Message="Email confirmed successfully"
                };
            }
            else
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Email did not confirmed"
                };
            }
        }

        public async Task<IActionResult> ResetEmail(string email)
        {
            User user = await _userManager.FindByEmailAsync(email);
            var confirmEmailToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            string validToken = await _tokenService.CreateValidToken(confirmEmailToken);

            string url = $"{_configuration["AppUrl"]}/new/pass?userid={user.Id}&token={validToken}&email={user.Email}";
            string content = $"<h1>Welcome to TMAS </h1><p>Click on link for reset your password  <a href='{url}'>Clicking here</a></p>";

            EmailOptions emailOptions = new EmailOptions
            {
                Email = user.Email,
                Content = content,
                Subject = "Reset password",
            };

            await _emailService.SendEmailAsync(emailOptions);

            return null;
        }

        public async Task<Response> ResetUserPassword(string userId, string token,string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return new Response
            {
                IsSuccess = false,
                Message = "User not found"
            };

            var checkPassword = await _userManager.CheckPasswordAsync(user, newPassword);
            if (checkPassword)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "You entered old password"
                };
            }
            var normalToken = await _tokenService.DecodingToken(token);
            var result = await _userManager.ResetPasswordAsync(user, normalToken, newPassword);

            if (result.Succeeded)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = "Password reseted successfully"
                };
            }
            else
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Password did not reseted"
                };
            }
        }

        public async Task<User> AddPhoto(Guid userId,string photo)
        {
            var findedUser = await _userManager.FindByIdAsync(userId.ToString());
            findedUser.Photo = photo;
            await _userManager.UpdateAsync(findedUser);
            return findedUser;
        }

       
    }
}
