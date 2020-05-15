using System.Configuration;
using System.Web.Mvc;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Net4MvcClient;
using NLog;
using NLog.Owin.Logging;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace Net4MvcClient
{   
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies"
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                Authority = "http://localhost:5000",
                ClientId = "net4mvcclient",
                ClientSecret = "secret3",
                RedirectUri = "http://localhost:49816/signin-oidc",//Net4MvcClient's URL
                ResponseType = "id_token",
                RequireHttpsMetadata = false,

                SignInAsAuthenticationType = "Cookies"
            });

            app.UseNLog((eventType) => LogLevel.Debug);
        }
    }
}
