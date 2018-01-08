using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExploreMidwest.Data;

namespace ExploreMidwest.Web.App_Start
{
    public class StartUp
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/auth/login")
            });
            app.CreatePerOwinContext(() => new ExploreMidwestDBContext());
            app.CreatePerOwinContext<UserManager<IdentityUser>>((options, context) => new UserManager<IdentityUser>(new UserStore<IdentityUser>(context.Get<ExploreMidwestDBContext>())));
            app.CreatePerOwinContext<RoleManager<IdentityRole>>((options, context) => new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context.Get<ExploreMidwestDBContext>())));
        }
    }
}

