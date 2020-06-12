using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.Owin;
using Microsoft.Owin.Security;
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
                
                PostLogoutRedirectUri = "http://localhost:49816",
                ResponseType = "id_token",
                RequireHttpsMetadata = false,

                TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    NameClaimType = "name"
                },

                SignInAsAuthenticationType = "Cookies",                

                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    SecurityTokenValidated = n =>
                    {
                        n.AuthenticationTicket.Identity.AddClaim(new Claim("id_token", n.ProtocolMessage.IdToken));
                        
                        return Task.FromResult(0);
                    },
                    RedirectToIdentityProvider = n =>
                    {
                        if (n.ProtocolMessage.RequestType == OpenIdConnectRequestType.Logout)
                        {
                            var id_token_claim = n.OwinContext.Authentication.User.Claims.FirstOrDefault(x => x.Type == "id_token");
                            if (id_token_claim != null)
                            {
                                n.ProtocolMessage.IdTokenHint = id_token_claim.Value;
                            }
                        }
                        return Task.FromResult(0);
                    }
                }
            });

            app.UseNLog((eventType) => LogLevel.Debug);
        }
    }
}
