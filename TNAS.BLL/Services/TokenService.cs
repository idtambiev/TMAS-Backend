using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.BLL.Interfaces;

namespace TMAS.BLL.Services
{
    public class TokenService:ITokenService
    {

        public async Task<string> CreateValidToken(string token)
        {
            var encodedEmailToken = Encoding.UTF8.GetBytes(token);
            var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);
            return validEmailToken;
        }

        public async Task<string> DecodingToken(string token)
        {
            var decodedToken = WebEncoders.Base64UrlDecode(token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);
            return normalToken;
        }
    }
}
