using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TMAS.DB.Models;
using TMAS.DB.Models.Enums;

namespace TMAS.Providers
{

        public interface IGoogleAuthProvider : IExternalAuthProvider
        {
            Provider Provider { get; }
        }

        public class GoogleAuthProvider<TUser> : IGoogleAuthProvider where TUser : IdentityUser, new( )
        {

            private readonly IHttpClientFactory _clientFactory;
            public GoogleAuthProvider(IHttpClientFactory clientFactory)
            {
                _clientFactory = clientFactory;
            }
            public Provider Provider => ProviderDataSource
                .GetProviders()
                .FirstOrDefault(x => x.Name.ToLower() == ProviderType.Google.ToString().ToLower());


            public async Task<JObject> GetUserInfo(string accessToken)
            {
                var request = new Dictionary<string, string>();

                request.Add("token", accessToken);
                var http = _clientFactory.CreateClient();
                var result = await http.GetAsync(Provider.UserInfoEndPoint + QueryBuilder.GetQuery(request, ProviderType.Google));

                if (result.IsSuccessStatusCode)
                {
                    var infoObject = JObject.Parse(await result.Content.ReadAsStringAsync());
                    return infoObject;
                }
                return null;
            }
        }
}
