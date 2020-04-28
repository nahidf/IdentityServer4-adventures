using System;
using System.Configuration;
using System.IdentityModel.Tokens;
using System.Web.Http;
using IdentityServer3.AccessTokenValidation;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Logging;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using NLog;
using NLog.Owin.Logging;
using Owin;

[assembly: OwinStartup(typeof(Net4Api.Startup))]
namespace Net4Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            JwtSecurityTokenHandler.InboundClaimTypeMap.Clear();

            app.UseIdentityServerBearerTokenAuthentication(
                new IdentityServerBearerTokenAuthenticationOptions
                {
                    Authority = "http://localhost:5000",
                    ValidationMode = ValidationMode.Local,
                    RequiredScopes = new[] { "api2" },
                    ClientSecret = "secret3",
                    ClientId = "api2"
                });

            //configure web api
            var config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            app.UseCors(CorsOptions.AllowAll);

            app.UseNLog((eventType) => LogLevel.Debug);

            app.UseWebApi(config);
        }
    }
}
