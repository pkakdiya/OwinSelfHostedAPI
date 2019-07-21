using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Owin.SelfHosted.API.OAuthServerProvider
{
    public class ApplicationOAuthServerProvider: OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            await Task.FromResult(context.Validated());
        }


        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            if (context.Password != "password")
            {
                context.SetError("invalid_grant", "Username and password are incorrect.");
                context.Rejected();
                return;
            }


            ClaimsIdentity identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("user_name", context.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
            context.Validated(identity);
        }
    }
}
