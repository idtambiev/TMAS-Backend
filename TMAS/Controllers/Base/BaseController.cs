using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TMAS.Controllers.Base
{
    public class BaseController:ControllerBase
    {
        protected Guid GetUserId()
        {
            var claims = HttpContext.User.Claims.ToList();
            string[] words = claims[4].ToString().Split(' ');
            Guid id = Guid.Parse(words[1]);
            return id;
        }
    }
}
