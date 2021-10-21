using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.BLL.Interfaces.BaseInterfaces;
using TMAS.DAL.DTO.View;
using TMAS.DB.Models;

namespace TMAS.BLL.Interfaces
{
    public interface IFileService: IBaseService
    {
        Task<File> Create(int cardId, string path, string type, string name);
        Task<IEnumerable<FileViewDTO>> GetFiles(int cardId);
    }
}
