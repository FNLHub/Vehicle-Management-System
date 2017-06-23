using Identity.Services.ActiveDirectory;
using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CosAuthenticationApi
{
    public class MyAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated(); //
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            string errorMessage = string.Empty;
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);

            if (!string.IsNullOrEmpty(context.UserName) && !string.IsNullOrEmpty(context.Password))
            {
                //Get the identity for the given user
                identity = AuthService.GetIdentity(context, ref errorMessage);

                if (identity != null && string.IsNullOrEmpty(errorMessage))
                {
                    //Valid identity found
                    context.Validated(identity);
                }
                else
                {
                    if (!string.IsNullOrEmpty(errorMessage))
                    {
                        context.SetError("invalid_grant", errorMessage);
                    }
                    else
                    {
                        context.SetError("invalid_grant", "Provided username and password is incorrect");
                    }
                }
            }
            else
            {
                context.SetError("invalid_grant", "Please provide a valid username and password");
            }
        }
    }
}