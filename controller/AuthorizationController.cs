using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace openiddictAPI.controller
{
    public class AuthorizationController :ControllerBase
    {
       [HttpPost("~/connect/token"), Produces("application/json")]
       public async Task<IActionResult> Exchange()
        {
            var request = HttpContext.GetOpenIddictServerRequest();
            if(request.IsPasswordGrantType())
            {
                var users = new Dictionary<string,string>
                {
                    {"emir","1234"}
                };


if(!users.TryGetValue(request.Username!, out var password) || password !=request.Password )
                {
                    return Forbid(
            authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
            properties: new AuthenticationProperties(new Dictionary<string, string?>
            {
                [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.InvalidGrant,
                [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "Kullanıcı adı veya şifre hatalı."
            }));
                }


            

            var identity = new ClaimsIdentity(TokenValidationParameters.
            DefaultAuthenticationType,
            Claims.Name,
            Claims.Role
            );
            identity.SetClaim(Claims.Subject, request.Username);
            identity.SetClaim(Claims.Name, request.Username);

            identity.SetDestinations(static claim => [Destinations.AccessToken]);


             return SignIn(new ClaimsPrincipal(identity), OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }
        throw new NotImplementedException("Bu grant type desteklenmiyor.");
        }

    }
}