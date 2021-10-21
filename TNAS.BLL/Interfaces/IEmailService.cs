using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.BLL.Interfaces.BaseInterfaces;
using TMAS.BLL.Models;
using TMAS.DB.Models;

namespace TMAS.BLL.Interfaces
{
    public interface IEmailService: IBaseService
    {
        Task SendEmailAsync(EmailOptions emailOptions);
    }
}
