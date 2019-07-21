using Microsoft.Owin.Hosting;
using Microsoft.Owin.Security.OAuth;
using Owin.SelfHosted.API.OAuthServerProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Owin.SelfHosted.API
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseURI = "http://localhost:8080";
            Console.WriteLine($"Web Server Started On: {baseURI}");
            WebApp.Start<StartUp>(baseURI);
            Console.WriteLine("Press Enter to Quit.");
            Console.ReadLine();
        }
    }

    public class StartUp
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            ConfigureOAuth(appBuilder);
            var webAPIConfiguration = ConfigureAPI();
            appBuilder.UseWebApi(webAPIConfiguration);
        }

        private void ConfigureOAuth(IAppBuilder builder)
        {
            var oAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new Microsoft.Owin.PathString("/Token"),
                Provider = new ApplicationOAuthServerProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(10),
                AllowInsecureHttp = true
            };

            builder.UseOAuthAuthorizationServer(oAuthOptions);
            builder.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }

        private HttpConfiguration ConfigureAPI()
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute("DefaultAPI", "api/{controller}/{id}", new { id = RouteParameter.Optional });
            return config;
        }
    }
}
