using System;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.OAuth;
using System.Web.Http;


[assembly: OwinStartup(typeof(MyProject.Startup))]

namespace CosAuthenticationApi
{
    public class Startup
    {
        /// <summary>
        /// POST to get the token of the given username and password.
        /// </summary>
        /// <param name="app"></param>
        /// <returns>Token</returns>
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            //Enable cros origin requests
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            var myProvider = new MyAuthorizationServerProvider();
            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions
            {
                //TODO: Set this to false for production.
                AllowInsecureHttp = false,
                //This is the path to get the token.
                TokenEndpointPath = new PathString("/token"),

                //Current token is 1 hour but we might extend this for the Facilities/Transportation Admins.
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(1),
                Provider = myProvider
            };

            app.UseOAuthAuthorizationServer(options);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
        }
    }
}
