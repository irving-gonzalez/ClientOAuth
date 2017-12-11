using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            app.UseEdSight(new EdSightOptions {
                ClientId ="1",
                ClientSecret="1"
            });

        }
    }
}