using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TMAS.DB.Models;
using TMAS.DB.Models.Enums;

namespace TMAS.Providers
{
    public class ExternalAuthenticationGrant:IExtensionGrantValidator
    {
        private readonly UserManager<User> _userManager;
        private readonly IGoogleAuthProvider _googleAuthProvider;
        private Dictionary<ProviderType, IExternalAuthProvider> _providers;
        public ExternalAuthenticationGrant(UserManager<User> userManager,IGoogleAuthProvider googleAuthProvider)
        {
            _userManager = userManager;
            _googleAuthProvider = googleAuthProvider;

            _providers = new Dictionary<ProviderType, IExternalAuthProvider> 
            {
                { ProviderType.Google,_googleAuthProvider }
            };
        }

        public string GrantType => "external";

        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            var provider = context.Request.Raw.Get("provider");
            if (string.IsNullOrWhiteSpace(provider))
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, "invalid provider");
                return;
            }
            var token = context.Request.Raw.Get("external_token");
            if (string.IsNullOrWhiteSpace(token))
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, "invalid external token");
                return;
            }

            var providerType = (ProviderType)Enum.Parse(typeof(ProviderType), provider, true);

            if (!Enum.IsDefined(typeof(ProviderType), providerType))
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, "invalid provider");
                return;
            }
            var userInfo = await _providers[providerType].GetUserInfo(token);
            if (userInfo == null)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, "couldn't retrieve user info from specified provider, please make sure that access token is not expired.");
                return;
            }

            var email = userInfo.Value<string>("email");
            var externalId = userInfo.Value<string>("id");

            if (!string.IsNullOrWhiteSpace(externalId))
            {
                // authenticate existing user
                var user = await _userManager.FindByLoginAsync(provider, externalId);
                if (user != null)
                {
                    var existingUser = await _userManager.FindByIdAsync(user.Id.ToString());
                    var userClaims = await _userManager.GetClaimsAsync(existingUser);
                    context.Result = new GrantValidationResult(existingUser.Id.ToString(), provider, userClaims, provider, null);
                    return;
                }
                else
                {
                    user = await _userManager.FindByEmailAsync(email);
                    if (user != null)
                    {
                        await _userManager.AddLoginAsync(user, new UserLoginInfo(provider, externalId, provider));
                        await _userManager.GetClaimsAsync(user);
                        await _userManager.SetLockoutEnabledAsync(user, false);
                        context.Result = new GrantValidationResult(user.Id.ToString(), GrantType);
                    }
                    else
                    {
                        user = new User
                        {   
                            Name=userInfo.Value<string>("given_name"),
                            Lastname = userInfo.Value<string>("family_name"),
                            UserName = userInfo.Value<string>("email"),
                            Email = userInfo.Value<string>("email"),
                            EmailConfirmed = true
                        };
                        var shouldCreate = await _userManager.CreateAsync(user);
                        await _userManager.AddLoginAsync(user, new UserLoginInfo(provider, externalId, provider));
                        await _userManager.GetClaimsAsync(user);
                        await _userManager.SetLockoutEnabledAsync(user, false);
                        user = await _userManager.FindByEmailAsync(email);
                        context.Result = new GrantValidationResult(user.Id.ToString(), GrantType);
                    }
                }
            }
            return;
        }
    }
}
