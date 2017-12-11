using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin;
using Owin;
using System.Globalization;

namespace ClientApp
{
    public static class EdSightExtension
    {

        public static IAppBuilder UseEdSight(this IAppBuilder app, EdSightOptions options)
        {
            app.Use<EdSightMiddleWare>(options);
            return app;
        }

    }


    public class EdSightOptions
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }

    public class EdSightMiddleWare : OwinMiddleware
    {
        public EdSightOptions options;
        public EdSightMiddleWare(OwinMiddleware next, EdSightOptions options) : base(next)
        {
            this.options = options;

            if (string.IsNullOrWhiteSpace(options.ClientId))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "ClientId"));
            }
            if (string.IsNullOrWhiteSpace(options.ClientSecret))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "ClientSecret"));
            }

        }

        public async override Task Invoke(IOwinContext context)
        {
            await this.Next.Invoke(context);

            //check if challenge for EdSight
            if (context?.Authentication.AuthenticationResponseChallenge != null)
                OnChallenge(context);
        }

        public void OnChallenge(IOwinContext context)
        {
            var challenge = context?.Authentication.AuthenticationResponseChallenge;
            if (challenge?.AuthenticationTypes.First(c => c.Equals("EdSight")) != null)
            {
                context.Response.Redirect("http://google.com");
            }
        }


    }
}