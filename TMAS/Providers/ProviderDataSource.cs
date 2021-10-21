using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMAS.DB.Models;
namespace TMAS.Providers
{
    public class ProviderDataSource
    {
        public static IEnumerable<Provider> GetProviders()
        {
            return new List<Provider>
            {
                new Provider
                {
                    ProviderId=1,
                    Name="Google",
                    UserInfoEndPoint="https://www.googleapis.com/oauth2/v2/userinfo"
                }
            };
        }
    }
}
