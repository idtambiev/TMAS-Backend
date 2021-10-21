using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DAL.Interfaces.BaseInterfaces;
using TMAS.DB.Models;

namespace TMAS.DAL.Interfaces
{
    public interface IFileRepository:IBaseRepository
    {
        Task<File> Create(File file);
        Task<IEnumerable<File>> GetFiles(int id);
    }
}
