using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.BLL.Interfaces.BaseInterfaces;

namespace TMAS.BLL.Interfaces
{
    public interface ITokenService:IBaseService
    {
        Task<string> CreateValidToken(string token);
        Task<string> DecodingToken(string token);

    }
}
