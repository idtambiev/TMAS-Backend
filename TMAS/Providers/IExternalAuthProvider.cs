using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TMAS.Providers
{
    public interface IExternalAuthProvider
    {
        Task<JObject> GetUserInfo(string accessToken);
    }
}
