using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.BLL.Interfaces.BaseInterfaces;
using TMAS.DAL.DTO;
using TMAS.DAL.DTO.Created;
using TMAS.DAL.DTO.View;
using TMAS.DB.Models;

namespace TMAS.BLL.Interfaces
{
    public interface IUserService:IBaseService
    {
        Task<User> GetOneByEmail(User user);
        Task<IEnumerable<UserDTO>> GetUsers(string searchText, Guid currentUserId, Guid creatorUserId);
        Task<UserCreatedDto> Create(UserCreatedDto createdUser);
        Task<Response> Find(string email);
        Task<UserDTO> GetOneById(Guid id);
        Task<Response> ConfirmEmailAsync(string id, string token);
        Task<Response> ResetUserPassword(string userId, string token, string newPassword);
        Task<User> AddPhoto(Guid userId, string photo);
        Task<IActionResult> ResetEmail(string email);
    }
}
