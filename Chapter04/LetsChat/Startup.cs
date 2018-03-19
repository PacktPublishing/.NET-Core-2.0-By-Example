using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace LetsChat
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //// Since HTTPS is secure, lets make it mandatory, by using the RequireHttpsAttribute Filter
            ////services.AddMvc();
            services.AddMvc(options =>
            {
                ////options.Filters.Add(new RequireHttpsAttribute());
            });

            
            //// Configure Authentication, we will challenge the user, via Facebook and sign in via Cookie authentication, so setting the appropriate values.
            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = FacebookDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
               .AddFacebook(options =>
               {
                   options.AppId = this.Configuration[Constants.FacebookAuthenticationAppId];     //// AppId & secret of registered facebook app.
                   options.AppSecret = this.Configuration[Constants.FacebookAuthenticationAppSecret];
               })
               .AddCookie();

            //// Register IUserTracker used by ChatHub.
            services.AddSingleton(typeof(IUserTracker), typeof(UserTracker));

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //// Redirect the request to HTTPS, if its Http.
            ////app.UseRewriter(new RewriteOptions().AddRedirectToHttps()); ////301, 44346

            app.UseStaticFiles();

            app.UseAuthentication();

            //// Use - SignalR & let it know to intercept and map any request having chatHub.
            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatHub>("chatHub");
            });

            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });        
        }
    }
}
